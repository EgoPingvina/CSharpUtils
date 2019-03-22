namespace CSharpUtils.Extensions
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class ModelsExt
    {
        /// <summary>
        /// Установка значений по умолчанию для всех вложенных свойств
        /// </summary>
        /// <typeparam name="T">Тип заплняемого экземпляра</typeparam>
        /// <param name="data">Экземпляр, поля которого необходимо "обнулить"</param>
        /// <returns>Исходный экземпляр, но с заполенными данными по умолчанию свойствами</returns>
        public static T SetDefaultValues<T>(this T data)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data))
            {
                if ((!property.PropertyType.IsPrimitive) && (property.PropertyType.Name != "DateTime") && (property.PropertyType.Name != "String"))
                    property.SetValue(
                        data,
                        Activator.CreateInstance(property.PropertyType)
                                 .SetDefaultValues());

                var myAttribute = (DefaultValueAttribute)property.Attributes[typeof(DefaultValueAttribute)];

                if (myAttribute != null)
                    property.SetValue(data, myAttribute.Value);
            }

            return data;
        }

        /// <summary>
        /// Преобразование модели в массив байтов
        /// </summary>
        /// <param name="source">Исходный объект</param>
        /// <param name="bySerualize">true-используя сериализацию, false-используя указатели</param>
        public static byte[] ToByteArray<T>(this T source, bool bySerialize = false)
        {
            if (bySerialize)
                using (var stream = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(stream, source);
                    return stream.ToArray();
                }
            else
            {
                var length = Marshal.SizeOf(source);
                var data = new byte[length];

                /* Получаем дескриптор массива, который будем заполнять. Запрещаем
                   GC перемещать объект в памяти. После получаем указатель на массив.
                 */
                var handle = GCHandle.Alloc(data, GCHandleType.Pinned);

                Marshal.StructureToPtr(source, handle.AddrOfPinnedObject(), false);

                handle.Free();

                return data;
            }
        }
    }
}