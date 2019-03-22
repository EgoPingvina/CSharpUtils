namespace CSharpUtils.WPF.MarkupExtensions.EnumExtensions
{
    using System;
    using System.Linq;
    using System.Windows.Markup;

    public class EnumToStringItemsSource : MarkupExtension
    {
        private readonly Type type;

        public EnumToStringItemsSource(Type type)
        {
            this.type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => Enum.GetValues(this.type)
                   .Cast<object>()
                   .Select(e => new
                   {
                       DisplayName  = e.ToString(),
                       Value        = e,
                   });
    }
}