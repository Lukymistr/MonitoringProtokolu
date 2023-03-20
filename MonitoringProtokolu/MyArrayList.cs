using System;
using System.Linq;
using System.Linq.Expressions;

namespace C_Sharp.ArrayList {
    internal class MyArrayList<T> { // T as type 
        private static T[] array = new T[0];

        public void add(T toAdd) {
            expand();
            array[array.Length - 1] = toAdd;
        }

        public void add(int position, T toAdd) {
            if (position < array.Length) {
                T[] tmp = new T[array.Length + 1];
                for (int i = 0; i < position; i++) {
                    tmp[i] = array[i];
                }
                tmp[position] = toAdd;
                for (int i = position; i < tmp.Length - 1; i++) {
                    int b = i + 1;
                    tmp[b] = array[i];
                }
                array = tmp;
                return;
            }
            T[] tmp2 = new T[position + 1];
            array.CopyTo(tmp2, 0);
            tmp2[position] = toAdd;
            array = tmp2;


        }

        public void addAll(T[] array) { 
            foreach(T t in array) { 
                add(t);
            }
        }

        public void set(int position, T value) {
            if (position > array.Length) {
                Console.WriteLine("Cannot set a value outside the length of the array");
                return;
            }
            array[position] = value;
        }

        private void expand() {
            T[] newArray = new T[array.Length + 1];
            array.CopyTo(newArray, 0);
            array = newArray;
        }

        public int size() {
            return array.Length;
        }

        public void remove(int position) {
            if (position > array.Length - 1) {
                Console.WriteLine("Cannot remove a value outside the length of the array");
                return;
            }
            int b = 0;
            T[] tmp = new T[array.Length - 1];
            for (int i = 0; i < position; i++) {
                tmp[i] = array[i];
                b++;
            }
            for (int i = b; i < tmp.Length; i++) {
                tmp[i] = array[b + 1];
                b++;
            }
            array = tmp;
        }

        override
        public String ToString() {
            String tmp = "[";
            for (int i = 0; i < array.Length; i++) {
                if (i + 1 == array.Length) {
                    tmp += array[i];
                } else {
                    tmp += array[i] + ", ";
                }
            }
            tmp += "]";
            return tmp;
        }

        public T get(int position) {
            if (position > array.Length - 1) {
                Console.Write("Cannot get a value outside the length of the array");
                return default;
            }
            return array[position];
        }

        public Boolean contains(T value) {
            for (int i = 0; i < array.Length; i++) {
                if (array[i].Equals(value)) {
                    return true;
                }
            }
            return false;
        }

        public void removeFirst(T value) {
            for (int i = 0; i < array.Length; i++) {
                if (value.Equals(array[i])) {
                    this.remove(i);
                    return;
                }
            }
            Console.WriteLine("The specified value was not found");
        }

        public void removeLast(T value) {
            for (int i = array.Length - 1; i >= 0; i--) {
                if (value.Equals(array[i])) {
                    this.remove(i);
                    return;
                }
            }
            Console.WriteLine("The specified value was not found");
        }

        public void removeAll(T value) {
            Boolean tmp = false;
            for (int i = 0; i < array.Length; i++) {
                if (value.Equals(array[i])) {
                    if (removeIf(i)) {
                        i--;
                        tmp = true;
                    }
                }
            }
            if (!tmp) {
                Console.WriteLine("The specified value was not found");
            }
        }

        public void clear() {
            T[] newArray = new T[0];
            array = newArray;
        }

        public void trimAll() {
            int b = 0;
            for (int i = 0; i < array.Length; i++) {
                if (array[i] == null || array[i].Equals('\u0000')) {
                    remove(i);
                    i--;
                }
            }
        }

        public void trim() {
            int i = 0;
            for (; i < array.Length; i++) {
                if (array[i] == null || array[i].Equals('\u0000')) {
                    remove(i);
                    i--;
                } else {
                    break;
                }
            }
            i = array.Length - 1;
            while (array[i] == null || array[i].Equals('\u0000')) {
                remove(i);
                i--;
            }
        }


        public Boolean isEmpty() {
            if (array.Length == 0) {
                return true;
            };
            return false;
        }

        public Boolean removeIf(int position) {
            if (position > array.Length - 1) {
                Console.WriteLine("Cannot remove a value outside the length of the array");
                return false;
            }
            int b = 0;
            T[] tmp = new T[array.Length - 1];
            for (int i = 0; i < position; i++) {
                tmp[i] = array[i];
                b++;
            }
            for (int i = b; i < tmp.Length; i++) {
                tmp[i] = array[b + 1];
                b++;
            }
            array = tmp;
            return true;
        }

        public String intoString() {
            String tmp = "";
            for (int i = 0; i < array.Length; i++)
            {
                tmp += array[i];
            }
            return tmp;
        }

        public T[] toArray()
        {
            return array;
        }

        public void toMyArrayList(T[] NewArray) {
            for (int i = 0; i < NewArray.Length; i++) {
                add(NewArray[i]);
            }
        }

        public void reverse() {
            T[] newArray = new T[array.Length]; 
            for (int i = 0; i < array.Length; i++) {
                newArray[i] = array[array.Length - i - 1];
            }
            array = newArray;
        }

        // TODO - jen pokud je to možné, aby to nehodilo error
        // Max
        // Min
        // sort
    }
}
