namespace CSharpUtils.Numeric
{
    public static class NumericConvertions
    {
        /// <summary>
        /// Обрезание значения value в пределах [min; max]
        /// </summary>
        /// <param name="min">Нижняя граница допустимого значения</param>
        /// <param name="max">Верхняя граница допустимого значения</param>
        /// <param name="value">Лимитируемое значение</param>
        /// <returns>Если value больше max, будет возвращено max, если меньше min-значение min, иначе-value останется без изменений</returns>
        public static int Limiter(int min, int max, int value)
            => value > min
                ? value < max
                    ? value
                    : max
                : min;

        /// <summary>
        /// Проверка на нахождение значения value в пределах [min; max]
        /// </summary>
        /// <param name="min">Нижняя граница допустимого значения</param>
        /// <param name="max">Верхняя граница допустимого значения</param>
        /// <param name="value">Проверяемое значение</param>
        /// <returns>value принадлежит отрезку [min; max] => true, иначе - false</returns>
        public static bool IsInRange(int min, int max, int value)
            => value >= min && value <= max;


        /// <summary>
        /// Преобразование(масштабирование) значения из одних пределов в другие
        /// </summary>
        /// <param name="minCurrent">Текущий нижний предел</param>
        /// <param name="maxCurrent">Текущий верхний предел</param>
        /// <param name="minNew">Новый нижний предел</param>
        /// <param name="maxNew">Новый верхний предел</param>
        /// <param name="value">Масштабируемое значение</param>
        /// <returns>Отмасштабированное значение в новых пределах</returns>
        public static int RangeTransform(int minCurrent, int maxCurrent, int minNew, int maxNew, int value)
            => (Limiter(minCurrent, maxCurrent, value) - minCurrent) * (maxNew - minNew) / (maxCurrent - minCurrent) + minNew;
    }
}