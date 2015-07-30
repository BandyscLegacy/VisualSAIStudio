using DynamicTypeDescriptor;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualSAIStudio
{

    [System.ComponentModel.Editor(typeof(StandardValueEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Flags]
    public enum SmartPhaseMask
    {
        [StandardValue("Always", Description = "Event will always occur")]
        Always = 0,

        [StandardValue("Phase 1", Description = "")]
        Phase_1 = 1,

        [StandardValue("Phase 2", Description = "")]
        Phase_2 = 2,

        [StandardValue("Phase 3", Description = "")]
        Phase_3 = 4,

        [StandardValue("Phase 4", Description = "")]
        Phase_4 = 8,

        [StandardValue("Phase 5", Description = "")]
        Phase_5 = 16,

        [StandardValue("Phase 6", Description = "")]
        Phase_6 = 32,

        [StandardValue("Phase 7", Description = "")]
        Phase_7 = 64,

        [StandardValue("Phase 8", Description = "")]
        Phase_8 = 128,

        [StandardValue("Phase 9", Description = "")]
        Phase_9 = 256,
    }

    [System.ComponentModel.Editor(typeof(StandardValueEditor), typeof(System.Drawing.Design.UITypeEditor))]
    [Flags]
    public enum SmartEventFlag
    {
        [StandardValue("NOT REPEATABLE", Description = "Event can not repeat")]
        SMART_EVENT_FLAG_NOT_REPEATABLE = 0x001,

        [StandardValue("DIFFICULTY 0", Description = "Event only occurs in normal dungeon")]
        SMART_EVENT_FLAG_DIFFICULTY_0 = 0x002,

        [StandardValue("DIFFICULTY 1", Description = "Event only occurs in heroic dungeon")]
        SMART_EVENT_FLAG_DIFFICULTY_1 = 0x004,

        [StandardValue("DIFFICULTY 2", Description = "Event only occurs in normal raid")]
        SMART_EVENT_FLAG_DIFFICULTY_2 = 0x008,

        [StandardValue("DIFFICULTY 3", Description = "Event only occurs in heroic raid")]
        SMART_EVENT_FLAG_DIFFICULTY_3 = 0x010,

        [StandardValue("DEBUG ONLY", Description = "Event only occurs in debug build")]
        SMART_EVENT_FLAG_DEBUG_ONLY = 0x080,

        [StandardValue("DONT RESET", Description = "Event will not reset in SmartScript::OnReset()")]
        SMART_EVENT_FLAG_DONT_RESET = 0x100,

        [StandardValue("DIFFICULTY ALL", Description = "")]
        SMART_EVENT_FLAG_DIFFICULTY_ALL = (SMART_EVENT_FLAG_DIFFICULTY_0 | SMART_EVENT_FLAG_DIFFICULTY_1 | SMART_EVENT_FLAG_DIFFICULTY_2 | SMART_EVENT_FLAG_DIFFICULTY_3),

        [StandardValue("ALL", Description = "All possible flags")]
        SMART_EVENT_FLAGS_ALL = (SMART_EVENT_FLAG_NOT_REPEATABLE | SMART_EVENT_FLAG_DIFFICULTY_ALL | SMART_EVENT_FLAG_DEBUG_ONLY | SMART_EVENT_FLAG_DONT_RESET),

    }

    public class SmartEvent : SmartElement
    {
        public event EventHandler ActionSelected = delegate { };
        public List<SmartCondition> conditions = new List<SmartCondition>();
        public List<SmartAction> actions = new List<SmartAction>();
        private ContextMenuStrip contextMenu = new ContextMenuStrip();
        private Point mouse;
        private bool dragging;

        public int chance {get; set;}
        public SmartPhaseMask phasemask { get; set; }
        public SmartEventFlag flags { get; set; }


        public SmartEvent(SmartGenericJSONData data) : base(data)
        {
            this.chance = 100;

            /** @TODO: MOVE TO ScratchWindow **/
            this.MouseDown += this_mouseDown;
            this.MouseUp += this_mouseUp;
            this.MouseMove += this_mouseMove;
            contextMenu.Items.Add("Copy", null, this_copyEvent);
            contextMenu.Items.Add("Paste", null, this_pasteEvent);
            contextMenu.Items.Add("Cut", null, this_cutEvent);
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Delete", null, this_deleteEvent);
        }

        public SmartAction GetAction(int index)
        {
            return actions[index];
        }

        public SmartCondition GetCondition(int index)
        {
            return conditions[index];
        }

        public SmartAction GetSelectedAction()
        {
            if (selectedChildren is SmartAction)
                return (SmartAction)selectedChildren;
            return null;
        }

        public SmartCondition GetSelectedCondition()
        {
            if (selectedChildren is SmartCondition)
                return (SmartCondition)selectedChildren;
            return null;
        }

        public List<SmartAction> GetActions()
        {
            return actions;
        }

        public void AddAction(SmartAction smartAction)
        {
            children.Add(smartAction);
            actions.Add(smartAction);
            smartAction.parent = this;
            smartAction.RequestUpdate += ActionRequestUpdateCallback;
        }

        public void InsertAction(SmartAction smartAction, int index)
        {
            children.Insert(index + conditions.Count, smartAction);
            actions.Insert(index, smartAction);
            smartAction.parent = this;
            smartAction.RequestUpdate += ActionRequestUpdateCallback;
            Invalide();
        }

        public void ReplaceAction(SmartAction replace, SmartAction search)
        {
            replace.Copy(search);

            int index = children.IndexOf(search);
            children.Remove(search);
            children.Insert(index, replace);

            index = actions.IndexOf(search);
            actions.Remove(search);
            actions.Insert(index, replace);

            replace.parent = this;
            replace.RequestUpdate += ActionRequestUpdateCallback;
            Invalide();
        }


        public void AddCondition(SmartCondition cond)
        {
            children.Add(cond);
            conditions.Add(cond);
            cond.parent = this;
            cond.RequestUpdate += ActionRequestUpdateCallback;
        }


        public void InsertCondition(SmartCondition smartCondition, int index)
        {
            children.Add(smartCondition);
            conditions.Insert(index, smartCondition);
            smartCondition.parent = this;
            smartCondition.RequestUpdate += ActionRequestUpdateCallback;
            Invalide();
        }


        public void ReplaceCondition(SmartCondition replace, SmartCondition search)
        {
            replace.Copy(search);

            int index = children.IndexOf(search);
            children.Remove(search);
            children.Insert(index, replace);

            index = conditions.IndexOf(search);
            conditions.Remove(search);
            conditions.Insert(index, replace);

            replace.parent = this;
            replace.RequestUpdate += ActionRequestUpdateCallback;
            Invalide();
        }


        public int GetInsertActionIndexFromPos(int x, int y)
        {
            if (actions.Count > 0 && y < actions[0].rect.Top + actions[0].rect.Height / 2)
                return 0;
            for (int i = actions.Count - 1; i >= 0; --i)
            {
                if (y > actions[i].rect.Top + actions[i].rect.Height/2)// && y < actions[i].rect.Bottom + 5)
                    return i+1;
            }
            return 0;
        }

        public int GetInsertConditionIndexFromPos(int x, int y)
        {
            if (conditions.Count > 0 && y > conditions[conditions.Count-1].rect.Bottom-5)
                return conditions.Count;
            for (int i = conditions.Count - 1; i >= 0; --i)
            {
                if (y > conditions[i].rect.Top - 5 && y < conditions[i].rect.Bottom + 5)
                    return i;
            }
            return 0;
        }

        private void this_cutEvent(object sender, EventArgs e)
        {
            this_copyEvent(sender, e);
            this_deleteEvent(sender, e);
        }

        private void this_pasteEvent(object sender, EventArgs e)
        {
            string[] array = Clipboard.GetText().Split('|');
            if (array.Length != 15)
                return;
            SmartAction doc = SmartAction.DeserializeFromArray(array);
            InsertAction(doc, GetInsertActionIndexFromPos(mouse.X, mouse.Y));
            doc.Invalide();
        }

        private void this_copyEvent(object sender, EventArgs e)
        {
            if (selectedChildren != null)
            {
                Clipboard.SetText(String.Join("|", ((SmartAction)selectedChildren).SerializeToArray()));
            }

        }

        private void this_deleteEvent(object sender, EventArgs e)
        {
            if (selectedChildren != null)
            {
                children.Remove(selectedChildren);
            }
            else
            {
                RequestRemove(this, new EventArgs());
            }
            Invalide();
        }

        private void this_mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouse.X = e.X;
                mouse.Y = e.Y;
                contextMenu.Show(System.Windows.Forms.Cursor.Position);
            }
        }

        private void this_mouseUp(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                if (selectedChildren is SmartAction)
                {
                    int index = GetInsertActionIndexFromPos(e.X, e.Y);
                    if (actions.IndexOf((SmartAction)selectedChildren) < index)
                        index--;
                    actions.Remove((SmartAction)selectedChildren);
                    actions.Insert(index, (SmartAction)selectedChildren);
                }
                else if (selectedChildren is SmartCondition)
                {
                    int index = GetInsertConditionIndexFromPos(e.X, e.Y);
                    if (conditions.IndexOf((SmartCondition)selectedChildren) < index)
                        index--;
                    conditions.Remove((SmartCondition)selectedChildren);
                    conditions.Insert(index, (SmartCondition)selectedChildren);
                }
                dragging = false;
            }
        }

        private void this_mouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X;
            mouse.Y = e.Y;
            if (e.Button == MouseButtons.Left)
                dragging = true;
        }

        public void Copy(SmartEvent prev)
        {
            base.Copy(prev);
            this.phasemask = prev.phasemask;
            this.flags = prev.flags;
            this.chance = prev.chance;
            this.conditions = prev.conditions;
            this.actions = prev.actions;
            ParameterValueChanged(this, new EventArgs());
        }

        protected override void ParameterValueChanged(object sender, EventArgs e)
        {
            if (readable == null)
                return;
            output = Smart.Format(readable, new
            {
                pram1 = parameters[0],
                pram2 = parameters[1],
                pram3 = parameters[2],
                pram4 = parameters[3],
                pram1value = parameters[0].GetValue(),
                pram2value = parameters[1].GetValue(),
                pram3value = parameters[2].GetValue(),
                pram4value = parameters[3].GetValue(),
            });
            if (chance < 100)
                output += " (" + chance + "% chance)";
            ChildrenModified(sender, e);
            base.ParameterValueChanged(sender, e);
        }

        public override Size ComputeSize(Graphics graphics, Font font, Font mini_font)
        {
            SizeF measure = graphics.MeasureString(ToString(), font);
            int width = (int)measure.Width + 30;
            children.ForEach(child => width = Math.Max(width, (int)child.ComputeSize(graphics, font, mini_font).Width));
            int height = (int)measure.Height;
            if (this.phasemask > SmartPhaseMask.Always)
                height += (int)graphics.MeasureString(ToString(), mini_font).Height;

            int children_height = 0;
            if (children.Count > 0)
            {
                children_height = children.Max(i => i.rect.Bottom) - rect.Top + 5;
                if (actions.Count > 0)
                    height = children_height;
                else if (conditions.Count > 0)
                    height += children_height;
            }
            else
                height += 5;

            return new Size(width, height);
        }

        public override Size Draw(Graphics graphics, int x, int y, int width, int height, Brush default_brush, Pen pen, Font font, Font mini_font, bool setRect = true)
        {
            Point start_pos = new Point(x, y);
            Size size = ComputeSize(graphics, font, mini_font);
            Brush brush = default_brush;
            if (selected)
            {
                brush = new SolidBrush(Color.FromArgb(255, 230, 230, 230));
                graphics.FillRectangle(brush, x, y, width, size.Height);
            }
            brush = default_brush;
          
            foreach (SmartCondition condition in conditions)
            {
                Size asize = condition.Draw(graphics, x+18, y+5, width, size.Height, default_brush, pen, font, mini_font, setRect);
                y += asize.Height;
            }
            
            graphics.FillEllipse(brush, x + 10, y + 10, 5, 5);
            graphics.DrawString(ToString(), font, brush, x + 18, y + 5);
            y += (int)graphics.MeasureString(ToString(), font).Height+5;
            if (phasemask > SmartPhaseMask.Always)
            {
                brush = Brushes.CadetBlue;
                graphics.DrawString("in "+phasemask.ToString(), mini_font, brush, x + 20, y);
                y += (int)graphics.MeasureString("in", mini_font).Height + 5;
            }

            foreach (SmartAction action in actions)
            {
                Size asize = action.Draw(graphics, x + 30, y, width, size.Height, default_brush, pen, font, mini_font, setRect);
                y += asize.Height;
            }

            if (dragging && selectedChildren != null)
                selectedChildren.Draw(graphics, mouse.X, mouse.Y, width, height, default_brush, pen, font, mini_font, false);
           
            if (setRect)
                SetRect(start_pos.X, start_pos.Y, width, size.Height);

            return size;
        }

        private void ActionRequestUpdateCallback(object sender, EventArgs e)
        {
            ChildrenModified(sender, e);
            Invalide();
        }

        public override bool IsDraggableArea(int x, int y)
        {
            if (conditions.Count > 0 && y < conditions[conditions.Count - 1].rect.Bottom)
                return false;

            if (actions.Count > 0 && y > actions[0].rect.Top)
                return false;

            return true;
        }

        internal void DragActionPaint(Graphics graphics, Brush brush, Pen pen, int x, int y)
        {
            if (children.Count == 0 || y < children[0].rect.Bottom)
            {
                graphics.DrawLine(pen, 30, rect.Y + 30, 200, rect.Y + 30);
            }
            else
            {
                foreach (DrawableElement child in children)
                {
                    int cury = child.rect.Bottom;
                    if (y > cury - 10 && y < cury + 10)
                    {
                        graphics.DrawLine(pen, 30, cury + 5, 200, cury + 5);
                        break;
                    }
                }
            }
        }

        public override List<Warning> Validate()
        {
            List<Warning> warnings = base.Validate();
            for (int i = 0; i < conditions.Count; ++i)
            {
                SmartCondition condition = GetCondition(i);
                warnings.AddRange(condition.Validate());
            }
            for (int i = 0; i < actions.Count;++i)
            {
                SmartAction action = GetAction(i);
                warnings.AddRange(action.Validate());
            }
            return warnings;
        }

    }



}
