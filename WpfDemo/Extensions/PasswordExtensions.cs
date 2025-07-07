using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDemo.Extensions
{
    /// <summary>
    /// 密码框不支持直接绑定, 需要使用附加属性来实现密码的绑定
    /// </summary>
    public class PasswordExtensions
    {
        // 获取附加属性Password的值
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        // 设置附加属性Password的值
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // 定义附加属性Password，类型为string
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordExtensions),
                new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        // 当Password属性发生变化时调用
        public static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            var newPassword = e.NewValue as string;
            // 如果是PasswordBox且新密码与当前密码不同，则更新PasswordBox的Password属性
            if (passwordBox != null && newPassword != passwordBox.Password)
            {
                passwordBox.Password = newPassword;
            }
        }
    }
    /// <summary>
    /// 用于同步PasswordBox的Password属性和附加属性Password的行为
    /// </summary>
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        // 附加行为时注册事件
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        // 当PasswordBox的Password属性变化时同步到附加属性
        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {

            PasswordBox? passwordBox = sender as PasswordBox;
            string password = PasswordExtensions.GetPassword(passwordBox);
            // 如果PasswordBox的Password与附加属性不同，则更新附加属性
            if (passwordBox != null && passwordBox.Password != password)
            {
                PasswordExtensions.SetPassword(passwordBox, passwordBox.Password);
            }
        }

        // 移除行为时注销事件
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }
}
