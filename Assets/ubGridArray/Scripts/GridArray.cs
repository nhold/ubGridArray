using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bifrost
{
    namespace Core
    {
        /// <summary>
        /// Contains a 1D array with accessors as a 2D array.
        /// This helps with the limitation of 2D arrays not being serializable by Unity.
        /// </summary>
        /// <typeparam name="T">The type to represent as a grid</typeparam>
        [Serializable]
        public class GridArray<T> : IEnumerable<T>
        {
            
            /// <summary>
            /// Default constructor for a grid array
            /// </summary>
            /// <param name="width">The width of the fake 2D array</param>
            /// <param name="height">The height of the fake 2D array</param>
            public GridArray(int width, int height)
            {
                arrayWidth = width;
                arrayHeight = height;

                this.array = new T[width * height];
            }

            
            private GridArray()
            {
            }

            /// <summary>
            /// Calculates the 1D array index for the fake 2D array
            /// </summary>
            /// <param name="X">The X value to access for the fake 2D array</param>
            /// <param name="Y">The Y value to access for the fake 2D array</param>
            /// <returns></returns>
            public int CalculateIndex(int X, int Y)
            {
                int index = Y * arrayWidth + X;

                if (index > (arrayWidth * arrayHeight) || index < 0)
                    throw new IndexOutOfRangeException("You tried to access outside of a GridArray range.");

                return index;
            }

            /// <summary>
            /// Are the given parameters a valid index?
            /// </summary>
            /// <param name="X">The X value of the fake 2D array</param>
            /// <param name="Y">The Y value of the fake 2D array</param>
            /// <returns></returns>
            public bool IsValidIndex(int X, int Y)
            {
                int index = Y * arrayWidth + X;

                if (index > (arrayWidth * arrayHeight) || index < 0)
                    return false;

                return true;
            }

            /// <summary>
            /// Gets the value at the fake 2D co-ordinates
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            /// <returns>The value at X and Y, throws an argument out of range exception if it failed.</returns>
            public T GetValueAt(int X, int Y)
            {
                T returnValue = default(T);

                try
                {
                    returnValue = array[CalculateIndex(X, Y)];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new ArgumentOutOfRangeException("Accessing outside of the range of index check your X and Y." + e.ToString());
                }

                return returnValue;
            }

            /// <summary>
            /// Sets the value at the fake 2D co-ordinates
            /// </summary>
            /// <param name="X">The X of the fake 2D array</param>
            /// <param name="Y">The Y of the fake 2D array</param>
            /// <param name="value">The value you want to set the index to</param>
            public void SetValueAt(int X, int Y, T value)
            {
                array[CalculateIndex(X, Y)] = value;
            }

            /// <summary>
            /// Index operator overload
            /// Has no safety so be careful
            /// </summary>
            /// <param name="i">What index to access</param>
            /// <returns></returns>
            public T this[int i]
            {
                get
                {
                    return array[i];
                }
                set
                {
                    array[i] = value;
                }
            }

            /// <summary>
            /// Index operator overload
            /// Has no safety so be careful
            /// </summary>
            /// <param name="x">What index to access</param>
            /// <param name="y">What index to access</param>
            /// <returns></returns>
            public T this[int x, int y]
            {
                get
                {
                    return GetValueAt(x,y);
                }
                set
                {
                    SetValueAt(x, y, value);
                }
            }

            /// <summary>
            /// Useful for checking bounds with the [] operator
            /// </summary>
            /// <returns>The number of elements in the array</returns>
            public int GetLength()
            {
                return array.Length;
            }

            /// <summary>
            /// Gets the width of the array.
            /// </summary>
            /// <returns></returns>
            public int GetWidth()
            {
                return arrayWidth;
            }

            /// <summary>
            /// Gets the height of the array
            /// </summary>
            /// <returns></returns>
            public int GetHeight()
            {
                return arrayHeight;
            }

            /// <summary>
            /// Recursive method which will fill the grid with the new value in 4 directions
            /// </summary>
            /// <param name="X"></param>
            /// <param name="Y"></param>
            /// <param name="value"></param>
            /// <param name="targetValue"></param>
            public void FloodFill(int X, int Y, T value, T targetValue)
            {
                if(IsValidIndex(X,Y))
                {
                    T target = GetValueAt(X, Y);

                    if (target.Equals(value))
                        return;

                    if (!target.Equals(targetValue))
                        return;

                    SetValueAt(X, Y, value);

                    FloodFill(X + 1, Y, value, targetValue);
                    FloodFill(X - 1, Y, value, targetValue);
                    FloodFill(X, Y + 1, value, targetValue);
                    FloodFill(X, Y - 1, value, targetValue);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Accessing outside of the range of index check your X and Y");
                }
            }

            /// <summary>
            /// Inherited from IEnumerable
            /// </summary>
            /// <returns>The enumerator</returns>
            public IEnumerator<T> GetEnumerator()
            {
                if (array == null)
                    return Enumerable.Empty<T>().GetEnumerator();
                return ((IEnumerable<T>)array).GetEnumerator();
            }

            /// <summary>
            /// Inherited from IEnumerable
            /// </summary>
            /// <returns>The enumerator</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            [SerializeField]
            private int arrayWidth;

            [SerializeField]
            private int arrayHeight;

            [SerializeField]
            private T[] array;

        }

    }
}
