I. Concept: 
  help coder creates runtime message or warning to assign description information
  into elements in source code (such as: Class, Method, Property, ...)

II. Feature: 
  - Attributes can be used on the following targets. The preceding examples show them on classes, 
    but they can also be used on: (Assembly, Class, Constructor, Delegate, Enum, 
                                Event, Field, GenericParameter, Interface, Method, 
                                Module, Parameter, Property, ReturnValue, Struct).
  - We Can create your own custom attribute
    Example:
            [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
            class DescriptionAttribute : Attribute
            {
                public string Description { get; set; }
                public DescriptionAttribute(string _description)
                {
                    Description = _description;
                }
            }
    in example above:
    AttributeUsage: You can specify where the attribute can 
    be used using the AttributeUsage attribute.
    if you not assign AttributeUsage, Attribute will be used for all ingredient

    * Some attributes in C#:
    - Obsolete: Marks a method, class, or property as obsolete and should not be used in new source code
    - Serializable: Marks a class as serializable(tuần tự hóa) and deserializable(giải tuần tự hóa)
                    into objects, typically for storage or transmission between different applications
    - DllImport: Marks a static method that is implemented by an external dynamic-link library
    - Conditional: Marks a method to specify a compile-time condition, and when marked, the method is only compiled if the condition is met
    - AttributeUsage:  Marks an attribute to specify how the attribute can be used, including usage targets, 
                         the number of times it can be used, and the objects it can be applied to
    - MethodImpl: Marks a method to specify its implementation details, including inlining (is delegate method?), synchronization, and other implementation options

    * Some attributes in .NET:
    - Required: Data cant be null
    - StringLength: Set length of input field
    - DataType: Indicates that data must be associated with a certain type
    - Range: Indicates that data needs to be within a range
    - Phone: Validate phone number
    - Email: Validate input email address