using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public class SmartEventsCollection : DrawableElementsCollection
    {


        public SmartEvent GetEvent(int index)
        {
            return (SmartEvent)collection[index];
        }


        public SmartEvent EventAt(int x, int y)
        {
            return (SmartEvent)base.ElementAt(x, y);
        }

        public DropResult GetDropResult(String type, int x, int y)
        {
            if (type.IndexOf("SMART_EVENT") > -1)
                return EventDropResult(x, y);
            else if (type.IndexOf("SMART_TARGET") > -1)
                 return TargetDropResult(x, y);
            else if (type.IndexOf("SMART_ACTION") > -1)
                return ActionDropResult(x, y);
            else if (type.IndexOf("CONDITION_") > -1)
                return ConditionDropResult(x, y);
            return DropResult.NONE;
        }

        private DropResult ConditionDropResult(int x, int y)
        {
            SmartEvent ev = EventAt(x, y);
            if (ev != null)
            {
                foreach (SmartCondition element in ev.conditions)
                {
                    if (y > (element.rect.Top + 5) && y < (element.rect.Bottom - 5))
                        return DropResult.REPLACE;
                }
                return DropResult.INSERT;
            }
            return DropResult.NONE;
        }

        private DropResult ActionDropResult(int x, int y)
        {
            SmartEvent ev = EventAt(x, y);
            if (ev != null)
            {
                foreach (SmartAction element in ev.actions)
                {
                    if (y > (element.rect.Top + 5) && y < (element.rect.Bottom - 5))
                        return DropResult.REPLACE;
                }
                return DropResult.INSERT;
            }
            return DropResult.NONE;
        }

        private DropResult TargetDropResult(int x, int y)
        {
            SmartEvent ev = EventAt(x, y);
            if (ev != null)
            {
                DrawableElement dr = ev.GetElementFromPos(x, y);
                if (dr != null)
                    return DropResult.REPLACE;
            }
            return DropResult.NONE;
        }

        private DropResult EventDropResult(int x, int y)
        {
            if (y < 5)
                return DropResult.INSERT;

            foreach (DrawableElement element in collection)
            {
                SmartEvent ev = (SmartEvent)element;
                if (y > (ev.rect.Top + 5) && y < (ev.rect.Bottom - 5))
                    return DropResult.REPLACE;

            }
            return DropResult.INSERT;

        }
    }

    public enum DropResult
    {
        INSERT,
        REPLACE,
        NONE
    }
}
