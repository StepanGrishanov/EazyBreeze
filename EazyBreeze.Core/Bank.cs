using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EazyBreeze.Core
{
    public class Bank
    {
        private int[] _windows;
        private Queue<int> _clients;
        private int? _freeWindowNumber
        {
            get
            {
                int? number = null;

                for (var i = 0; i < _windows.Count(); i++)
                    if (!WindowsIsWorked(_windows[i]))
                        number = i;

                return number;
            }
        }

        private bool _haveFreeWindows { get => _freeWindowNumber != null; }
        private bool _allWindowsAreFree { get => _windows.All(x => !WindowsIsWorked(x)); }
        private bool _haveClients { get => _clients.Any(); }


        public Bank(int windowsCount)
        {
            _windows = new int[windowsCount];
            _clients = new Queue<int>();
        }

        public void AddClient(int duration)
        {
            _clients.Enqueue(duration);
        }

        public int Work()
        {
            var duration = 0;

            do
            {
                SeetClientIfNeed();
                duration += WorkOrWait();
            }
            while (_haveClients || !_allWindowsAreFree);

            return duration;
        }

        private void SeetClientIfNeed()
        {
            if (!_haveClients)
                return;

            var client = _clients.Dequeue();
            _windows[_freeWindowNumber.Value] = client;
        }

        private int WorkOrWait()
        {
            if (_haveFreeWindows && _haveClients)
                return 0;

            var minDuration = _windows.Where(x => WindowsIsWorked(x)).Min();

            for (var i = 0; i < _windows.Count(); i++)
            {
                if (!WindowsIsWorked(_windows[i]))
                    continue;

                _windows[i] = _windows[i] - minDuration;
            }

            return minDuration;
        }

        private bool WindowsIsWorked(int duration)
        {
            return duration != 0;
        }
    }
}
