namespace CSharpUtils.WPF.MarkupExtensions.EnumExtensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Markup;

    public class EnumToItemsSource : MarkupExtension
    {
        private readonly Type type;

        public EnumToItemsSource(Type type)
        {
            this.type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => Enum.GetValues(this.type)
                   .Cast<object>()
                   .Select(e => new EnumPresenter
                   {
                       DisplayName  = (this.type.GetField(e.ToString())
                                                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                .FirstOrDefault() as DescriptionAttribute)
                                                ?.Description ?? e.ToString(),
                       Value        = e
                   });

        /// <summary>
        /// Модель для передачи значений типа Enum графическим элементам
        /// </summary>
        public class EnumPresenter
        {
            /// <summary>
            /// Отображаемое имя элемента
            /// </summary>
            public string DisplayName { get; set; }

            /// <summary>
            /// Соответствующее значение из enum-а
            /// </summary>
            public object Value { get; set; }
        }
    }
}