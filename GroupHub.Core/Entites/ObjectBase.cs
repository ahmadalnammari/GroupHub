using System.ComponentModel.Composition.Hosting;


namespace GroupHub.Core
{ 
    public abstract class ObjectBase //:  IExtensibleDataObject
    {
        public ObjectBase()
        {
            //_Validator = GetValidator();
            //Validate();
        }

        protected bool _IsDirty = false;
        

        public static CompositionContainer Container { get; set; }

        

        

    }
}
