using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio
{
    public abstract class SmartTarget
    {
        public int ID { get; private set; }

        public string help { get; private set; }

        public float[] position = new float[4];
        public Parameter[] parameters = new Parameter[3];

        public SmartTarget(int id, String tooltip)
        {
            this.ID = id;
            this.help = tooltip;
            for (int i = 0; i < 3; ++i)
                parameters[i] = NullParameter.GetInstance();
        }

        public abstract String GetReadableString();

        public string GetCoords()
        {
            return String.Format("({0}, {1}, {2}, {3})", position[0], position[1], position[2], position[3]);
        }

        public virtual void UpdateParams(int index, int value)
        {
            parameters[index].SetValue(value);
        }

        public void UpdateParams(SmartTarget smartTarget)
        {
            for (int i = 0; i < 3;++i)
                parameters[i].SetValue(smartTarget.parameters[i].GetValue());

            for (int i = 0; i < 4; ++i)
                position[i] = smartTarget.position[i];
        }


        public string GetCPPCode()
        {
            return "ObjectList* targets = new ObjectList();";
        }
    }
}
