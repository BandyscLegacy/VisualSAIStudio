using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    public class SQLGenerator
    {
        
        //private readonly static string SQL = "({entryorguid}, {source_type}, {id,2}, {linkto,2}, {event_id, 2}, {phasemask}, {chance, 3}, {flags}, {event_param1,5}, {event_param2,5}, {event_param3,5}, {event_param4,5}, {action_id}, {action_param1,5}, {action_param2,5}, {action_param3,5}, {action_param4,5}, {action_param5,2}, {action_param6,2}, {target_id}, {target_param1,5}, {target_param2,5}, {target_param3,5}, {target_position}, \"{comment}\")";
        private readonly static string SQL = "({entryorguid}, {source_type}, {id}, {linkto}, {event_id}, {phasemask}, {chance}, {flags}, {event_param1}, {event_param2}, {event_param3}, {event_param4}, {action_id}, {action_param1}, {action_param2}, {action_param3}, {action_param4}, {action_param5}, {action_param6}, {target_id}, {target_param1}, {target_param2}, {target_param3}, {target_position}, \"{comment}\")";
        
        //private readonly static string SQL = "                            ({entryorguid,13}, {source_type,13}, {id,4}, {linkto,6}, {event_id, 12}, {phasemask,18}, {chance, 3}, {flags}, {event_param1,5}, {event_param2,5}, {event_param3,5}, {event_param4,5}, {action_id}, {action_param1,5}, {action_param2,5}, {action_param3,5}, {action_param4,5}, {action_param5,2}, {action_param6,2}, {target_id}, {target_param1,5}, {target_param2,5}, {target_param3,5}, {target_position}, \"{comment}\")";

        private static string GenerateHeader(SAIType source_type, int entryorguid)
        {
            StringBuilder ret = new StringBuilder();
            
            int guid = 0;
            int entry = entryorguid;

            if (entryorguid < 0)
            {
                guid = -entryorguid;
                entry = 0;
            }

            string name = null;

            switch (source_type)
            {
                case SAIType.Creature:
                    name = StringsDB.GetInstance().Get(StorageType.Creature, entry);
                    break;
            }
            
            ret.AppendLine("-- "+name+" "+entry +" SAI");
            ret.AppendLine("SET @ENTRY := "+entry+";");
            ret.AppendLine("UPDATE `creature_template` SET `AIName`=\"SmartAI\" WHERE `entry`= "+entry+";");
            ret.AppendLine("DELETE FROM `smart_scripts` WHERE `entryorguid`=@ENTRY AND `source_type`="+(int)source_type+";");
            ret.AppendLine("INSERT INTO `smart_scripts` (`entryorguid`, `source_type`, `id`, `link`, `event_type`, `event_phase_mask`, `event_chance`, `event_flags`, `event_param1`, `event_param2`, `event_param3`, `event_param4`, `action_type`, `action_param1`, `action_param2`, `action_param3`, `action_param4`, `action_param5`, `action_param6`, `target_type`, `target_param1`, `target_param2`, `target_param3`, `target_x`, `target_y`, `target_z`, `target_o`, `comment`) VALUES");
            return ret.ToString();
        }

        public static string GenerateSAISQL(SAIType source_type, int entryorguid, SmartEventsCollection events)
        {
            int event_id = 0;
            List<string> lines = new List<string>();
            foreach (SmartEvent e in events)
            {

                lines.Add(GenerateSingle(source_type, entryorguid, event_id, e, e.GetAction(0), (e.GetActions().Count == 1 ? 0 : event_id + 1)));

                event_id++;

                for (int index = 1; index < e.GetActions().Count; ++index)
                {
                    lines.Add(GenerateSingle(source_type, entryorguid, event_id, Events.EVENT_LINK.GetInstance(), e.GetAction(index), (e.GetActions().Count -1 == index ? 0 : event_id + 1)));
                    event_id++;
                }

            }
            return GenerateHeader(source_type, entryorguid)+"\n"+String.Join(",\n", lines) + ";";
        }


        private static string GenerateSingle(SAIType sourcetype, int entry, int id, SmartEvent ev, SmartAction action, int link = 0)
        {
            object data = new
            {
                entryorguid = entry.ToString(),
                source_type = ((int)sourcetype).ToString(),
                id = id.ToString(),
                linkto = link.ToString(),

                event_id = ev.ID.ToString(),
                phasemask = ((int)ev.phasemask).ToString(),
                chance = ev.chance.ToString(),
                flags = ((int)ev.flags).ToString(),
                event_param1 = ev.parameters[0].GetValue().ToString(),
                event_param2 = ev.parameters[1].GetValue().ToString(),
                event_param3 = ev.parameters[2].GetValue().ToString(),
                event_param4 = ev.parameters[3].GetValue().ToString(),

                action_id = action.ID.ToString(),
                action_param1 = action.parameters[0].GetValue().ToString(),
                action_param2 = action.parameters[1].GetValue().ToString(),
                action_param3 = action.parameters[2].GetValue().ToString(),
                action_param4 = action.parameters[3].GetValue().ToString(),
                action_param5 = action.parameters[4].GetValue().ToString(),
                action_param6 = action.parameters[5].GetValue().ToString(),

                target_id = action.target.ID.ToString(),
                target_param1 = action.target.parameters[0].GetValue().ToString(),
                target_param2 = action.target.parameters[1].GetValue().ToString(),
                target_param3 = action.target.parameters[2].GetValue().ToString(),


                target_position = String.Join(", ", action.target.position),

                comment = CommentGenerator.GenerateComment(ev, action)
            };

            return SmartFormat.Smart.Format(SQL, data);
        }

    }
}
