using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSAIStudio.SmartScripts;

namespace VisualSAIStudio
{
    public static class TargetsFactory
    {
        public static SmartTarget Factory(String name)
        {
            switch (name)
            {
                case "SMART_TARGET_NONE":
                    return new SMART_TARGET_NONE();
                case "SMART_TARGET_SELF":
                    return new SMART_TARGET_SELF();
                case "SMART_TARGET_VICTIM":
                    return new SMART_TARGET_VICTIM();
                case "SMART_TARGET_HOSTILE_SECOND_AGGRO":
                    return new SMART_TARGET_HOSTILE_SECOND_AGGRO();
                case "SMART_TARGET_HOSTILE_LAST_AGGRO":
                    return new SMART_TARGET_HOSTILE_LAST_AGGRO();
                case "SMART_TARGET_HOSTILE_RANDOM":
                    return new SMART_TARGET_HOSTILE_RANDOM();
                case "SMART_TARGET_HOSTILE_RANDOM_NOT_TOP":
                    return new SMART_TARGET_HOSTILE_RANDOM_NOT_TOP();
                case "SMART_TARGET_ACTION_INVOKER":
                    return new SMART_TARGET_ACTION_INVOKER();
                case "SMART_TARGET_POSITION":
                    return new SMART_TARGET_POSITION();
                case "SMART_TARGET_CREATURE_RANGE":
                    return new SMART_TARGET_CREATURE_RANGE();
                case "SMART_TARGET_CREATURE_GUID":
                    return new SMART_TARGET_CREATURE_GUID();
                case "SMART_TARGET_CREATURE_DISTANCE":
                    return new SMART_TARGET_CREATURE_DISTANCE();
                case "SMART_TARGET_STORED":
                    return new SMART_TARGET_STORED();
                case "SMART_TARGET_GAMEOBJECT_RANGE":
                    return new SMART_TARGET_GAMEOBJECT_RANGE();
                case "SMART_TARGET_GAMEOBJECT_GUID":
                    return new SMART_TARGET_GAMEOBJECT_GUID();
                case "SMART_TARGET_GAMEOBJECT_DISTANCE":
                    return new SMART_TARGET_GAMEOBJECT_DISTANCE();
                case "SMART_TARGET_INVOKER_PARTY":
                    return new SMART_TARGET_INVOKER_PARTY();
                case "SMART_TARGET_PLAYER_RANGE":
                    return new SMART_TARGET_PLAYER_RANGE();
                case "SMART_TARGET_PLAYER_DISTANCE":
                    return new SMART_TARGET_PLAYER_DISTANCE();
                case "SMART_TARGET_CLOSEST_CREATURE":
                    return new SMART_TARGET_CLOSEST_CREATURE();
                case "SMART_TARGET_CLOSEST_GAMEOBJECT":
                    return new SMART_TARGET_CLOSEST_GAMEOBJECT();
                case "SMART_TARGET_CLOSEST_PLAYER":
                    return new SMART_TARGET_CLOSEST_PLAYER();
                case "SMART_TARGET_ACTION_INVOKER_VEHICLE":
                    return new SMART_TARGET_ACTION_INVOKER_VEHICLE();
                case "SMART_TARGET_OWNER_OR_SUMMONER":
                    return new SMART_TARGET_OWNER_OR_SUMMONER();
                case "SMART_TARGET_THREAT_LIST":
                    return new SMART_TARGET_THREAT_LIST();
            }
            return null;
        }



        public static SmartTarget Factory(int id)
        {
            switch (id)
            {
                case 0:
                    return new SMART_TARGET_NONE();
                case 1:
                    return new SMART_TARGET_SELF();
                case 2:
                    return new SMART_TARGET_VICTIM();
                case 3:
                    return new SMART_TARGET_HOSTILE_SECOND_AGGRO();
                case 4:
                    return new SMART_TARGET_HOSTILE_LAST_AGGRO();
                case 5:
                    return new SMART_TARGET_HOSTILE_RANDOM();
                case 6:
                    return new SMART_TARGET_HOSTILE_RANDOM_NOT_TOP();
                case 7:
                    return new SMART_TARGET_ACTION_INVOKER();
                case 8:
                    return new SMART_TARGET_POSITION();
                case 9:
                    return new SMART_TARGET_CREATURE_RANGE();
                case 10:
                    return new SMART_TARGET_CREATURE_GUID();
                case 11:
                    return new SMART_TARGET_CREATURE_DISTANCE();
                case 12:
                    return new SMART_TARGET_STORED();
                case 13:
                    return new SMART_TARGET_GAMEOBJECT_RANGE();
                case 14:
                    return new SMART_TARGET_GAMEOBJECT_GUID();
                case 15:
                    return new SMART_TARGET_GAMEOBJECT_DISTANCE();
                case 16:
                    return new SMART_TARGET_INVOKER_PARTY();
                case 17:
                    return new SMART_TARGET_PLAYER_RANGE();
                case 18:
                    return new SMART_TARGET_PLAYER_DISTANCE();
                case 19:
                    return new SMART_TARGET_CLOSEST_CREATURE();
                case 20:
                    return new SMART_TARGET_CLOSEST_GAMEOBJECT();
                case 21:
                    return new SMART_TARGET_CLOSEST_PLAYER();
                case 22:
                    return new SMART_TARGET_ACTION_INVOKER_VEHICLE();
                case 23:
                    return new SMART_TARGET_OWNER_OR_SUMMONER();
                case 24:
                    return new SMART_TARGET_THREAT_LIST();
            }
            return new UNKNOWN_TARGET(id);
        }
    }
}
