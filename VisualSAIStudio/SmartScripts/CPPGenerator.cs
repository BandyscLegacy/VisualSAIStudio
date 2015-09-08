using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    public class CPPGenerator
    {


        public static string Indent(string str)
        {
            string[] lines = str.Split('\n');

            StringBuilder output = new StringBuilder();
            int indent_level = 0;

            foreach (string line in lines)
            {
                if (line.Contains("}"))
                    indent_level--;


                output.Append(new String(' ', 4 * indent_level));


                if (line.Contains("{"))
                    indent_level++;
                output.AppendLine(line.TrimEnd());
            }
            return output.ToString();
        }


        public static string GenerateScriptedAIHeader(string npc_name, string content)
        {
            npc_name = "npc_"+npc_name.ToLower().Replace(' ','_');
            string source = @"class {npc} : public CreatureScript
{obra}
public:
    {npc}() : CreatureScript(""{npc}"") {obra} {cbra}

struct {npc}_AI : public ScriptedAI
{obra}
    {npc}_AI(Creature* creature) : ScriptedAI(creature) {obra} {cbra}
{content}
{cbra};

CreatureAI* GetAI(Creature* creature) const override
{obra}
return new {npc}_AI(creature);
{cbra}
{cbra};";
            return SmartFormat.Smart.Format(source, new { content = content, npc = npc_name, obra = "{", cbra = "}" });
        }

        public static string GenerateSAICpp(SAIType source_type, int entryorguid, SmartEventsCollection events)
        {
            Dictionary<EventMethod, List<SmartEvent>> methods = new Dictionary<EventMethod, List<SmartEvent>>();
            StringBuilder output = new StringBuilder();
            foreach (SmartEvent ev in events)
            {
                SmartGenericJSONData generic_data = SmartFactory.GetInstance().GetGenericData(SmartType.SMART_EVENT, ev.ID);

                if (!methods.ContainsKey(generic_data.event_method))
                    methods.Add(generic_data.event_method, new List<SmartEvent>());

                methods[generic_data.event_method].Add(ev);
            }


            foreach (EventMethod event_method in methods.Keys)
            {
                cppMethodAttribute method_data = Extensions.GetAttribute<cppMethodAttribute>(event_method);
                InvokerAttribute invoker_data = Extensions.GetAttribute<InvokerAttribute>(event_method);
                output.AppendLine(method_data.method);
                output.AppendLine("{");
                string invoker = "me";
                if (invoker_data != null)
                    invoker = invoker_data.invoker;

                int id = 0;
                foreach (SmartEvent ev in methods[event_method])
                {
                    SmartGenericJSONData generic_data = SmartFactory.GetInstance().GetGenericData(SmartType.SMART_EVENT, ev.ID);
                    id++;

                    string actions = GenerateActionsCpp(ev.actions, invoker);

                    object data = new
                    {
                        pram1value = ev.parameters[0].GetValue().ToString(),
                        pram2value = ev.parameters[1].GetValue().ToString(),
                        pram3value = ev.parameters[2].GetValue().ToString(),
                        pram4value = ev.parameters[3].GetValue().ToString(),
                        content = actions,
                        content_in_brackets = "{\n" + actions + "\n}",
                        no_id = id.ToString(),
                        cbra = "}",
                        obra = "{",
                    };

                    output.AppendLine(SmartFormat.Smart.Format(generic_data.GetCpp(), data));
                }

                output.AppendLine("}\n");
            }
            string npc =DB.GetInstance().GetString(DB.GetInstance().StorageForType(SAIType.Creature, entryorguid < 0), entryorguid);
            return Indent(GenerateScriptedAIHeader(npc, output.ToString()));
        }

        private static string GenerateActionsCpp(List<SmartAction> actions, string invoker)
        {
            List<string> output = new List<string>();

            foreach (SmartAction action in actions)
            {
                SmartGenericJSONData generic_data = SmartFactory.GetInstance().GetGenericData(SmartType.SMART_ACTION, action.ID);
                SmartGenericJSONData generic_target_data = SmartFactory.GetInstance().GetGenericData(SmartType.SMART_TARGET, action.Target.ID);
                object data = new
                {
                    pram1value = action.parameters[0].GetValue().ToString(),
                    pram2value = action.parameters[1].GetValue().ToString(),
                    pram3value = action.parameters[2].GetValue().ToString(),
                    pram4value = action.parameters[3].GetValue().ToString(),
                    pram5value = action.parameters[2].GetValue().ToString(),
                    pram6value = action.parameters[3].GetValue().ToString(),
                    target = (generic_target_data.target_cpp == "invoker" ? invoker : generic_target_data.target_cpp),
                    cbra = "}",
                    obra = "{",
                };

                string content = SmartFormat.Smart.Format(generic_data.GetCpp(), data);

                object data2 = new
                    {
                        pram1value = action.Target.parameters[0].GetValue().ToString(),
                        pram2value = action.Target.parameters[1].GetValue().ToString(),
                        pram3value = action.Target.parameters[2].GetValue().ToString(),
                        content = content,
                        content_in_brackets = "{\n"+content+"\n}",
                        cbra = "}",
                        obra = "{",
                    };

                
                output.Add(SmartFormat.Smart.Format(generic_target_data.GetCpp(), data2));
            }

            return String.Join("\n", output);
        }
    }
}
