namespace CSharpUtils.Numeric
{
    using System;

    /// <summary>
    /// Число в закольцованном интервале (аббревиатура от "number in the looped interval")
    /// </summary>
    public class NuLI
    {
        /// <summary>
        /// Нижний предел интервала
        /// </summary>
        private readonly int min;

        /// <summary>
        /// Верхний предел интервала
        /// </summary>
        private readonly int max;

        /// <summary>
        /// Длина закольцованного интервала
        /// </summary>
        private readonly int length;

        /// <summary>
        /// Текущее значение
        /// </summary>
        private int current;

        /// <summary>
        /// Явное преобразование в int
        /// </summary>
        public static explicit operator int(NuLI nuli)
            => nuli.current;

        #region Addition

        public static NuLI operator +(NuLI leftOperand, int rightOperand)
        {
            int result = leftOperand.current + rightOperand;
            leftOperand.Normalize(ref result);

            return new NuLI(leftOperand.min, leftOperand.max, result);
        }

        public static NuLI operator +(int leftOperand, NuLI rightOperand)
            => rightOperand + leftOperand;

        public static NuLI operator +(NuLI leftOperand, NuLI rightOperand)
        {
            if (!leftOperand.IsWithinLimits(rightOperand))
                throw new ArgumentException("Limits are not equal", "rightOperand.min or/and rightOperand.max");

            return leftOperand + (int)rightOperand;
        }

        #endregion

        #region Subtraction

        public static NuLI operator -(NuLI leftOperand, int rightOperand)
        {
            int result = leftOperand.current - rightOperand;
            leftOperand.Normalize(ref result);

            return new NuLI(leftOperand.min, leftOperand.max, result);
        }

        public static NuLI operator -(int leftOperand, NuLI rightOperand)
        {
            int result = leftOperand - rightOperand.current;
            rightOperand.Normalize(ref result);

            return new NuLI(rightOperand.min, rightOperand.max, result);
        }

        public static NuLI operator -(NuLI leftOperand, NuLI rightOperand)
        {
            if (!leftOperand.IsWithinLimits(rightOperand))
                throw new ArgumentException("Limits are not equal", "rightOperand.min or/and rightOperand.max");

            return leftOperand - (int)rightOperand;
        }

        #endregion

        #region Multiplication

        public static NuLI operator *(NuLI leftOperand, int rightOperand)
        {
            int result = leftOperand.current * rightOperand;
            leftOperand.Normalize(ref result);

            return new NuLI(leftOperand.min, leftOperand.max, result);
        }

        public static NuLI operator *(int leftOperand, NuLI rightOperand)
            => rightOperand * leftOperand;

        public static NuLI operator *(NuLI leftOperand, NuLI rightOperand)
        {
            if (!leftOperand.IsWithinLimits(rightOperand))
                throw new ArgumentException("Limits are not equal", "rightOperand.min or/and rightOperand.max");

            return leftOperand * (int)rightOperand;
        }

        #endregion

        #region Division

        public static NuLI operator /(NuLI leftOperand, int rightOperand)
        {
            int result = leftOperand.current / rightOperand;
            leftOperand.Normalize(ref result);

            return new NuLI(leftOperand.min, leftOperand.max, result);
        }

        public static NuLI operator /(int leftOperand, NuLI rightOperand)
        {
            int result = leftOperand / rightOperand.current;
            rightOperand.Normalize(ref result);

            return new NuLI(rightOperand.min, rightOperand.max, result);
        }

        public static NuLI operator /(NuLI leftOperand, NuLI rightOperand)
        {
            if (!leftOperand.IsWithinLimits(rightOperand))
                throw new ArgumentException("Limits are not equal", "rightOperand.min or/and rightOperand.max");

            return leftOperand / (int)rightOperand;
        }

        #endregion

        /// <summary>
        /// Инициализация пределов, исходного значения и необходимости "обрезания" передаваемого значения по указанным пределам
        /// </summary>
        /// <param name="min">Нижняя граница интервала</param>
        /// <param name="max">Верхняя гарница интервала</param>
        /// <param name="value">Исходное значение</param>
        /// <param name="isNeedLimit">Признак необходимости обрезания value по установленным пределам</param>
        public NuLI(int min, int max, int value, bool isNeedLimit = true)
        {
            this.length = 
                (this.max = max) - (this.min = min);

            this.current = isNeedLimit
                ? NumericConvertions.Limiter(this.min, this.max, value)
                : value >= this.min && value <= this.max
                    ? value
                    : throw new ArgumentOutOfRangeException("value not in [min; max]");
        }

        /// <summary>
        /// В одних ли пределах находятся текущее и переданное значения
        /// </summary>
        /// <param name="other">Сравниваемый объект</param>
        /// <returns>true-в одних пределах, false-в разных</returns>
        public bool IsWithinLimits(NuLI other)
            => this.min == other.min && this.max == other.max;

        /// <summary>
        /// Нормализация значения value в пределах [min; max]
        /// </summary>
        /// <param name="value">Нормализуемое значение</param>
        private void Normalize(ref int value)
        {
            while (value < this.min)
                value += this.length;

            while (value > this.max)
                value -= this.length;
        }
    }
}