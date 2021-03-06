﻿namespace CSharpUtils.WPF.MarkupExtensions.EnumExtensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Markup;

    /* Example:
     * 
     * 
     * <UserControl xmlns:extxaml="clr-namespace:CSharpUtils.WPF.MarkupExtensions.EnumExtensions"
     *              xmlns:enums="*path to your enum location*">
     * ***
     *  <ComboBox ItemsSource="{extxaml:EnumToIntItemsSource {x:Type enums:*enum name*}}"/>
     *  ***
     *  </UserControl>
     */

    public class EnumToIntItemsSource : MarkupExtension
    {
        private readonly Type type;

        public EnumToIntItemsSource(Type type)
        {
            this.type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => Enum.GetValues(this.type)
                   .Cast<object>()
                   .Select(e => new
                   {
                       DisplayName  = (this.type.GetField(e.ToString())
                                                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                .First() as DescriptionAttribute)
                                                .Description,
                       Value        = (int)e
                   });
    }
}