//using Microsoft.Xaml.Behaviors;
//using System.Reflection;
//using System.Windows;

//namespace WpfTechnoTester.Triggers
//{
//    public class CallUnaryMethodAction : TargetedTriggerAction<DependencyObject>
//    {
//        // The name of the method to invoke.
//        public static readonly DependencyProperty MethodNameProperty =
//            DependencyProperty.Register("CloseIfHasSaved",
//                typeof(string),
//                typeof(CallUnaryMethodAction),
//                new PropertyMetadata(OnNeedsMethodInfoUpdated));

//        public string CloseIfHasSaved
//        {
//            get { return (string)GetValue(MethodNameProperty); }
//            set { SetValue(MethodNameProperty, value); }
//        }

//        // Flag that lets us determine if we want to search non-public methods in our target object.
//        public static readonly DependencyProperty AllowNonPublicMethodsProperty =
//            DependencyProperty.Register("AllowNonPublicMethods",
//                typeof(bool),
//                typeof(CallUnaryMethodAction),
//                new PropertyMetadata(OnNeedsMethodInfoUpdated));

//        public bool AllowNonPublicMethods
//        {
//            get { return (bool)GetValue(AllowNonPublicMethodsProperty); }
//            set { SetValue(AllowNonPublicMethodsProperty, value); }
//        }

//        // Parameter we want to pass to our method. If this has not been set, then the value passed
//        // to the trigger action's Invoke method will be used instead.
//        public static readonly DependencyProperty ParameterProperty =
//            DependencyProperty.Register("Parameter",
//                typeof(object),
//                typeof(CallUnaryMethodAction));

//        public object Parameter
//        {
//            get { return GetValue(ParameterProperty); }
//            set { SetValue(ParameterProperty, value); }
//        }

//        private static void OnNeedsMethodInfoUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            var action = d as CallUnaryMethodAction;
//            if (action != null)
//                action.UpdateMethodInfo();
//        }

//        protected override void OnAttached()
//        {
//            UpdateMethodInfo();
//        }

//        protected override void OnTargetChanged(DependencyObject oldTarget, DependencyObject newTarget)
//        {
//            UpdateMethodInfo();
//        }

//        protected override void Invoke(object parameter)
//        {
//            object target = this.TargetObject ?? this.AssociatedObject;
//            if (target == null)
//                return;

//            // Determine what we are going to pass to our method.
//            object methodParam = ReadLocalValue(ParameterProperty) == DependencyProperty.UnsetValue ?
//                parameter : this.Parameter;

//            // Pick the best method to call given the parameter we want to pass.
//            Method methodToCall = m_methods.FirstOrDefault(method =>
//                (methodParam != null) && method.ParameterInfo.ParameterType.IsAssignableFrom(methodParam.GetType()));

//            if (methodToCall == null)
//                throw new InvalidOperationException("No suitable method found.");

//            methodToCall.MethodInfo.Invoke(target, new object[] { methodParam });
//        }

//        private void UpdateMethodInfo()
//        {
//            m_methods.Clear();
//            object target = this.TargetObject ?? this.AssociatedObject;
//            if (target == null || string.IsNullOrEmpty(this.))
//                return;

//            // Find all unary methods with the given name.
//            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
//            if (this.AllowNonPublicMethods)
//                flags |= BindingFlags.NonPublic;

//            foreach (MethodInfo methodInfo in target.GetType().GetMethods(flags))
//            {
//                if (methodInfo.Name == this.MethodName)
//                {
//                    ParameterInfo[] parameters = methodInfo.GetParameters();
//                    if (parameters.Length == 1)
//                        m_methods.Add(new Method(methodInfo, parameters[0]));
//                }
//            }

//            // Order the methods so that methods with most derived parameters are ordered first.
//            // This will help us pick the most appropriate method in the call to Invoke.
//            m_methods = m_methods.OrderByDescending<Method, int>(method =>
//            {
//                int rank = 0;
//                for (Type type = method.ParameterInfo.ParameterType; type != typeof(object); type = type.BaseType)
//                    ++rank;
//                return rank;
//            }).ToList<Method>();
//        }

//        private List<Method> m_methods = new List<Method>();

//        // Holds info on the list of possible methods we can call.
//        private class Method
//        {
//            public Method(MethodInfo methodInfo, ParameterInfo paramInfo)
//            {
//                this.MethodInfo = methodInfo;
//                this.ParameterInfo = paramInfo;
//            }

//            public MethodInfo MethodInfo { get; private set; }
//            public ParameterInfo ParameterInfo { get; private set; }
//        }
//    }
//}