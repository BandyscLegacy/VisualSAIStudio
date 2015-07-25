using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace VisualSAIStudio
{

    class ModalEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            IEnumerable<StorageTypeAttribute> attrs = context.PropertyDescriptor.Attributes.OfType<StorageTypeAttribute>();

            if (attrs.Count<StorageTypeAttribute>() > 0 && svc != null)
            {
                using (ChooseRowFromDBForm form = new ChooseRowFromDBForm(attrs.First <StorageTypeAttribute>().type)) 
                {
                    form.Value = (int)value;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                        value = form.Value; 
                }
            }
            return value; 
        }
    }


    public class DataFromDBConverter : TypeConverter
    {
        public DataFromDBConverter() : base() {  }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                int ret;
                if (int.TryParse(value.ToString(), out ret))
                {
                    return ret;
                }
                return 0;
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            IEnumerable<StorageTypeAttribute> attrs = context.PropertyDescriptor.Attributes.OfType<StorageTypeAttribute>();
            if (attrs.Count<StorageTypeAttribute>() > 0)
            {
                StorageType type = attrs.First<StorageTypeAttribute>().type;
                string name = StringsDB.GetInstance().Get(type, int.Parse(value.ToString()));
                if (name == null)
                    return "Unknown " + type + " (" + value + ")";
                else
                    return name + " (" + value + ")";
            }
           return base.ConvertFrom(context, culture, value);
        }

    }

    class StorageTypeAttribute : Attribute
    {
        public StorageType type { get; protected set; }
        public StorageTypeAttribute(StorageType type)
        {
            this.type = type;
        }
    }
}
