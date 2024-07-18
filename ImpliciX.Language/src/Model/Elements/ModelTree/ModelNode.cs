using System;
using System.Collections.Concurrent;
namespace ImpliciX.Language.Model
{
    public abstract class ModelNode
    {
        private readonly string _urnToken;
        private readonly ConcurrentDictionary<Type, PrivateModelNode> _privateModelNodes;
        protected ModelNode(string urnToken, ModelNode parent)
        {
            _urnToken = urnToken;
            Parent = parent;
            _privateModelNodes = new ConcurrentDictionary<Type, PrivateModelNode>();
            Urn = Parent == null ? Urn.BuildUrn(_urnToken) : Urn.BuildUrn(Parent.Urn, _urnToken);

        }

        public Urn Urn { get; }

        public ModelNode Parent { get; }

        public string Token => _urnToken;
        
        public T _private<T>() where T:PrivateModelNode
        {
            return (T) _privateModelNodes.GetOrAdd(
                    typeof(T), 
                    _ => (T) Activator.CreateInstance(typeof(T), new object[] {this})); 
        }

        public override string ToString()
        {
            return Urn.ToString();
        }

        protected bool Equals(ModelNode other)
        {
            return Equals(Urn, other.Urn);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ModelNode)obj);
        }

        public override int GetHashCode()
        {
            return (Urn != null ? Urn.GetHashCode() : 0);
        }
    }
}