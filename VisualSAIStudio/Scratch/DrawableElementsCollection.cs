using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualSAIStudio
{
    public class DrawableElementsCollection : IEnumerable<DrawableElement> 
    {
        public event EventHandler ElementAdded = delegate { };
        public event EventHandler ElementRemoved = delegate { };
        public event EventHandler ElementInserted = delegate { };
        public event EventHandler ElementsChanged = delegate { };

        protected List<DrawableElement> collection = new List<DrawableElement>();

        public int Count
        {
            get
            {
                return collection.Count;
            }
        }

        public DrawableElement this[int key]
        {
            get
            {
                return collection[key];
            }
            set
            {
                collection[key] = value;
            }
        }

        public void Clear()
        {
            collection.Clear();
            ElementsChanged(this, new EventArgs());
        }

        public void Add(DrawableElement element)
        {
            collection.Add(element);
            ElementAdded(this, new EventArgs());
            ElementsChanged(this, new EventArgs());
        }

        public DrawableElement ElementAt(int x, int y)
        {
            foreach (DrawableElement element in collection)
            {
                if (element.contains(x, y))
                    return element;
            }
            return null;
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
            ElementRemoved(this, new EventArgs());
            ElementsChanged(this, new EventArgs());
        }

        public void Replace(DrawableElement search, DrawableElement replace)
        {
            int index = collection.IndexOf(search);
            collection.Remove(search);
            collection.Insert(index, replace);
            ElementsChanged(this, new EventArgs());
        }

        public void MoveTo(DrawableElement element, int index)
        {
            collection.Remove(element);
            collection.Insert(index, element);
            ElementsChanged(this, new EventArgs());
        }

        public void Insert(DrawableElement element, int index)
        {
            collection.Insert(index, element);
            ElementInserted(this, new EventArgs());
            ElementsChanged(this, new EventArgs());
        }

        public void Remove(DrawableElement element)
        {
            collection.Remove(element);
            ElementRemoved(this, new EventArgs());
            ElementsChanged(this, new EventArgs());
        }

        public DrawableElement Get(int index)
        {
            return collection[index];
        }

        public IEnumerator<DrawableElement> GetEnumerator()
        {
            foreach (DrawableElement element in collection)
                yield return element;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
