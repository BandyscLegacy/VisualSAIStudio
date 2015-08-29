using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    class SmartFactory
    {

        public Dictionary<SmartType, Dictionary<int, SmartGenericJSONData>> smart_id_data = new Dictionary<SmartType, Dictionary<int, SmartGenericJSONData>>();

        public Dictionary<SmartType, Dictionary<string, int>> smart_name_to_id = new Dictionary<SmartType,Dictionary<string,int>>(); 

        private static Dictionary<string, IList<SAIType>> events_name_types = new Dictionary<string, IList<SAIType>>();

        public void Add(SmartType type, SmartGenericJSONData data)
        {
            if (!smart_id_data.ContainsKey(type))
            {
                smart_id_data[type] = new Dictionary<int, SmartGenericJSONData>();
                smart_name_to_id[type] = new Dictionary<string,int>();
            }

            if (!smart_id_data[type].ContainsKey(data.id))
            {
                smart_id_data[type].Add(data.id, data);
                smart_name_to_id[type].Add(data.name, data.id);
                if (type == SmartType.SMART_EVENT)
                    events_name_types.Add(data.name, data.valid_types);
            }

        }

        public bool SmartExist(SmartType type, string id)
        {
            if (!smart_id_data.ContainsKey(type))
                return false;

            return (smart_name_to_id.ContainsKey(type) && smart_name_to_id[type].ContainsKey(id));
        }

        public SmartEvent EventFactory(int id)
        {
            if (smart_id_data.ContainsKey(SmartType.SMART_EVENT) && smart_id_data[SmartType.SMART_EVENT].ContainsKey(id))
                return new SmartEvent(smart_id_data[SmartType.SMART_EVENT][id]);
            return null;
        }
        public SmartEvent EventFactory(string id)
        {
            if (smart_name_to_id.ContainsKey(SmartType.SMART_EVENT) && smart_name_to_id[SmartType.SMART_EVENT].ContainsKey(id))
                return EventFactory(smart_name_to_id[SmartType.SMART_EVENT][id]);
            return null;
        }

        public SmartCondition ConditionFactory(int id)
        {
            if (smart_id_data.ContainsKey(SmartType.SMART_CONDITION) && smart_id_data[SmartType.SMART_CONDITION].ContainsKey(id))
                return new SmartCondition(smart_id_data[SmartType.SMART_CONDITION][id]);
            return null;
        }
        public SmartCondition ConditionFactory(string id)
        {
            if (smart_name_to_id.ContainsKey(SmartType.SMART_CONDITION) && smart_name_to_id[SmartType.SMART_CONDITION].ContainsKey(id))
                return ConditionFactory(smart_name_to_id[SmartType.SMART_CONDITION][id]);
            return null;
        }

        public SmartTarget TargetFactory(int id)
        {
            if (smart_id_data.ContainsKey(SmartType.SMART_TARGET) && smart_id_data[SmartType.SMART_TARGET].ContainsKey(id))
                return new SmartTarget(smart_id_data[SmartType.SMART_TARGET][id]);
            return null;
        }
        public SmartTarget TargetFactory(string id)
        {
            if (smart_name_to_id.ContainsKey(SmartType.SMART_TARGET) && smart_name_to_id[SmartType.SMART_TARGET].ContainsKey(id))
                return TargetFactory(smart_name_to_id[SmartType.SMART_TARGET][id]);
            return null;
        }

        public SmartAction ActionFactory(int id)
        {
            if (smart_id_data.ContainsKey(SmartType.SMART_ACTION) && smart_id_data[SmartType.SMART_ACTION].ContainsKey(id))
                return new SmartAction(smart_id_data[SmartType.SMART_ACTION][id]);
            return null;
        }
        public SmartAction ActionFactory(string id)
        {
            if (smart_name_to_id.ContainsKey(SmartType.SMART_ACTION) && smart_name_to_id[SmartType.SMART_ACTION].ContainsKey(id))
                return ActionFactory(smart_name_to_id[SmartType.SMART_ACTION][id]);
            return null;
        }



        public bool IsEventValidType(string id, SAIType saitype)
        {
            if (events_name_types.ContainsKey(id))
            {
                if (events_name_types[id] != null && events_name_types[id].Contains(saitype))
                    return true;
                return false;
            }
            return true;
        }


        private static SmartFactory instance;
        public static SmartFactory GetInstance()
        {
            if (instance==null)
                instance = new SmartFactory();
            return instance;
        }
    }
}
