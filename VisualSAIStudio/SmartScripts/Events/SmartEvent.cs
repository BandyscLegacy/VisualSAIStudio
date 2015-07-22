using DynamicTypeDescriptor;
using SmartFormat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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

    public abstract class SmartEvent : SmartElement
    {
        public event EventHandler ActionSelected = delegate { };
        private List<SmartCondition> conditions = new List<SmartCondition>();

        public int chance {get; set;}
        public SmartPhaseMask phasemask { get; set; }
        public SmartEventFlag flags { get; set; }

        protected override void UpdateParamsInternal(int index, int value)
        {
            this.parameters[index].SetValue(value);
        }

        public SmartEvent() : base() { }

        public SmartEvent(int id) : base() 
        {
            this.ID = id;
        }

        public SmartEvent(int id, string name)
        {
            this.ID = id;
            this.name = name;
        }

        public void Copy(SmartEvent prev)
        {
            base.Copy(prev);
            this.conditions = prev.conditions;
        }

        public void AddCondition(SmartCondition cond)
        {
            conditions.Add(cond);
        }

        internal void AddAction(SmartAction smartAction)
        {
            children.Add(smartAction);
            smartAction.parent = this;
            smartAction.RequestUpdate += ActionRequestUpdateCallback;
        }

        private void ActionRequestUpdateCallback(object sender, EventArgs e)
        {
            Invalide();
        }

        public string GetCPPCode()
        {
            StringBuilder ret = new StringBuilder();

            foreach (DrawableElement child in children)
            {
                SmartAction action = (SmartAction)child;
                ret.AppendLine(action.GetCPPCode());
            }

            return ret.ToString();
        }

        public SmartAction GetAction(int index)
        {
            return (SmartAction)children[index];
        }

        public SmartAction GetSelectedAction()
        {
            return (SmartAction)selectedChildren;
        }

        public List<DrawableElement> GetActions()
        {
            return children;
        }

        protected override void paramValueChanged(object sender, EventArgs e)
        {
            if (GetReadableString() == null)
                return;
            readable = Smart.Format(GetReadableString(), new
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
            base.paramValueChanged(sender, e);
        }

        public override Size Draw(Graphics graphics, int x, int y, int width, int height, Brush brush, Pen pen, Font font, bool setRect = true)
        {
            Point start_pos = new Point(x, y);
            Size size = ComputeSize(graphics, font);
            int maxX = size.Width;

            if (selected)
            {
                brush = new SolidBrush(Color.FromArgb(255, 230, 230, 230));
                graphics.FillRectangle(brush, x, y, width, size.Height);
            }
            brush = Brushes.Black;
            graphics.FillEllipse(brush, x + 10, y + 10, 5, 5);
            foreach (SmartCondition condition in conditions)
            {
                graphics.DrawString(condition.GetReadableString(), font, brush, x + 18, y + 5);
                y += (int)graphics.MeasureString(condition.GetReadableString(), font).Height + 10;
            }
            graphics.DrawString(ToString(), font, brush, x + 18, y + 5);
            y += (int)graphics.MeasureString(ToString(), font).Height + 10;
            foreach (DrawableElement child in children)
            {
                Size asize = child.Draw(graphics, x + 30, y, width, size.Height, brush, pen, font, setRect);
                y += asize.Height;
            }
            
            if (setRect)
                SetRect(start_pos.X, start_pos.Y, maxX, size.Height);

            return size;
        }

        public void ReplaceAction(SmartAction replace, SmartAction search)
        {
            int index = children.IndexOf(search);
            replace.Copy(search);
            children.Remove(search);
            children.Insert(index, replace);
            replace.parent = this;
            replace.RequestUpdate += ActionRequestUpdateCallback;
            Invalide();
        }

        public int GetInsertIndexFromPos(int x, int y)
        {
            if (y > rect.Bottom - 5)
                return children.Count;
            for (int i = children.Count - 1; i >= 0; --i )
            {
                if (y > children[i].rect.Top - 5 && y < children[i].rect.Bottom + 5)
                    return i;
            }
            return 0;
        }

        public void InsertAction(SmartAction smartAction, int index)
        {
            children.Insert(index, smartAction);
            smartAction.parent = this;
            smartAction.RequestUpdate += ActionRequestUpdateCallback;
            Invalide();
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
            for (int i = 0; i < children.Count;++i)
            {
                SmartAction action = GetAction(i);
                warnings.AddRange(action.Validate());
            }
            return warnings;
        }

        public override string GetReadableString()
        {
            throw new NotImplementedException();
        }
    }



}
