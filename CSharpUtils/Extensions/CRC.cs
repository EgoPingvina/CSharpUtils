namespace CSharpUtils.Extensions
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Методы для подсчёта и проверки контрольных сумм
    /// </summary>
    public static class CRC
    {
        /// <summary>
        /// Подсчёт контрольной суммы (XOR)
        /// </summary>
        /// <param name="source">Сообщение, контрольную сумму которого необходимо найти</param>
        public static byte XOR(this IEnumerable<byte> source)
        {
            byte result = 0x00;     // Набиваем единицами начальную контрольную сумму

            // Производим сложение по модулю 2(xor) над каждым байтом данных
            source.ForEach(b => result ^= b);

            return result;
        }

        /// <summary>
        /// Подсчёт контрольной суммы (CRC16)
        /// </summary>
        /// <param name="source">Сообщение, контрольную сумму которого необходимо найти</param>
        public static ushort CRC16(this IEnumerable<byte> source)
        {
            ushort crc = 0xFFFF;                            // Забиваем единицами crc сумму

            source.ForEach(b =>
            {
                crc ^= (ushort)(b << 8);                    // Сдвигаем байт на старший разряд

                for (var i = 0; i < 8; i++)
                {
                    var isNull = crc & 0x8000;              // Маска. Проверяем, что старший бит равен 1

                    // Если старший бит единица-сдвигаем вправо и операция XOR, иначе только сдвиг
                    crc = isNull != 0
                        ? (ushort)((crc << 1) ^ 0x1021)
                        : (ushort)(crc << 1);
                }
            });

            return crc;
        }

        /// <summary>
        /// Подсчёт контрольной суммы (CRC16)
        /// </summary>
        /// <param name="text">Сообщение, контрольную сумму которого необходимо найти</param>
        public static ushort CRC16(this string text)
            => Encoding.ASCII.GetBytes(text).CRC16();

        /// <summary>
        /// Подсчёт контрольной суммы (CRC32)
        /// </summary>
        /// <param name="sorce">Сообщение, контрольную сумму которого необходимо найти</param>
        public static uint CRC32(this IEnumerable<byte> source)
        {
            var crcTable = new uint[256];
            uint crc = 0;

            for (uint i = 0; i < 256; i++)
            {
                crc = i;

                for (uint j = 0; j < 8; j++)
                {
                    var isNull = crc & 1;           // Маска. Проверяем, что старший бит равен 1

                    crc = isNull != 0
                        ? (crc >> 1) ^ 0xEDB88320
                        : crc >> 1;
                }

                crcTable[i] = crc;
            };

            crc = 0xFFFFFFFF;                       // Забиваем 1 crc сумму

            source.ForEach(b =>
                crc = crcTable[(crc ^ b) & 0xFF] ^ (crc >> 8));

            return crc ^ 0xFFFFFFFF;
        }

        /// <summary>
        /// Подсчёт контрольной суммы (CRC32)
        /// </summary>
        /// <param name="text">Сообщение, контрольную сумму которого необходимо найти</param>
        public static uint CRC32(this string text)
            => Encoding.ASCII.GetBytes(text).CRC32();

        /// <summary>
        /// Проверка контрольной суммы (XOR)
        /// </summary>
        /// <param name="source">Сообщение, контрольную сумму которого необходимо проверить</param>
        /// <param name="crc">Предполагаемая контрольная сумма</param>
        /// <returns>Если контрольные суммы совпали, возвращает true, иначе - false</returns>
        public static bool ChechXOR(this IEnumerable<byte> source, byte crc)
            => source.XOR() == crc;

        /// <summary>
        /// Проверка контрольной суммы (CRC16)
        /// </summary>
        /// <param name="source">Сообщение, контрольную сумму которого необходимо проверить</param>
        /// <param name="crc">Предполагаемая контрольная сумма</param>
        /// <returns>Если контрольные суммы совпали, возвращает true, иначе - false</returns>
        public static bool CheckCRC16(this IEnumerable<byte> source, ushort crc)
            => source.CRC16() == crc;

        /// <summary>
        /// Проверка контрольной суммы (CRC16)
        /// </summary>
        /// <param name="text">Сообщение, контрольную сумму которого необходимо проверить</param>
        /// <param name="crc">Предполагаемая контрольная сумма</param>
        /// <returns>Если контрольные суммы совпали, возвращает true, иначе - false</returns>
        public static bool CheckCRC16(this string text, ushort crc)
            => text.CRC16() == crc;

        /// <summary>
        /// Проверка контрольной суммы (CRC32)
        /// </summary>
        /// <param name="source">Сообщение, контрольную сумму которого необходимо проверить</param>
        /// <param name="crc">Предполагаемая контрольная сумма</param>
        /// <returns>Если контрольные суммы совпали, возвращает true, иначе - false</returns>
        public static bool CheckCRC32(this IEnumerable<byte> source, uint crc)
            => source.CRC32() == crc;

        /// <summary>
        /// Проверка контрольной суммы (CRC32)
        /// </summary>
        /// <param name="text">Сообщение, контрольную сумму которого необходимо проверить</param>
        /// <param name="crc">Предполагаемая контрольная сумма</param>
        /// <returns>Если контрольные суммы совпали, возвращает true, иначе - false</returns>
        public static bool CheckCRC32(this string text, uint crc)
            => text.CRC32() == crc;
    }
}