using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SmartScripts
{
    public class CommentGenerator
    {
        public static string GenerateComment(SmartEvent ev, SmartAction action)
        {
            StringBuilder comment = new StringBuilder();


            comment.Append(ev);
            comment.Append(" - ");
            comment.Append(action);
            comment.Append(" // " + action.Comment);


            return comment.ToString();
        }
    }
}
