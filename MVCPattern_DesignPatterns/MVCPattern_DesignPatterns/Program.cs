using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVCPattern_DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public interface IBeatModel
        {
            void Initialize();
            void on();
            void off();
            void setBPM(int bpm);
            void getBPM();
            void registerObserver(BeatObserver observer);
            void removeObserver(BeatObserver observer);

            void registerObserver(BPMObserver observer);
            void removeObserver(BPMObserver observer);
        }

        public class BeatModel: IBeatModel
        {
            IList<BeatObserver>  beatObservers = new BindingList<BeatObserver>();
            IList<BPMObserver> beatObservers = new BindingList<BPMObserver>();
            private int BPM = 0;
            private Sequencer sequencer;

            public void Initialize()
            {
                sequencer = new Sequencer();
                sequencer.Setup();
            }

            public void on()
            {
                sequencer.Start();
                setBPM(90);
            }

            public void off()
            {
                setBPM(0);
                sequencer.Stop();
            }

            public void setBPM(int bpm)
            {
                BPM = bpm;
                notifiBPMObservers();
            }

            public void getBPM()
            {
                return BPM;
            }

            void beatEvent()
            {
                notifyBeatOvsevers();
            }

            void IBeatModel.registerObserver(BeatObserver observer)
            {
                registerObserver(observer);
            }

            void IBeatModel.removeObserver(BPMObserver observer)
            {
                removeObserver(observer);
            }

            void IBeatModel.removeObserver(BeatObserver observer)
            {
                removeObserver(observer);
            }

            void IBeatModel.registerObserver(BPMObserver observer)
            {
                registerObserver(observer);
            }
        }
    }
}
