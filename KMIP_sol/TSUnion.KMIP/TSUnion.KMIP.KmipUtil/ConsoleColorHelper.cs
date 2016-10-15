using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSUnion.KMIP.KmipUtil
{
    abstract class State
    {
        protected ConsoleColor Color;
        public abstract void Handle(Context context, ConsoleColor color);
        
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class ChangedColor : State
    {
        public override void Handle(Context context, ConsoleColor color)
        {
            Color = color;
            context.State = new DefaultColor();
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class DefaultColor : State
    {
        
        public override void Handle(Context context, ConsoleColor color)
        {
            Color = color;
            context.State = new ChangedColor();
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    class Context
    {
        private State _state;

        // Constructor
        public Context(State state)
        {
            this.State = state;
        }

        // Gets or sets the state
        public State State
        {
            get { return _state; }
            set
            {
                _state = value;
                
            }
        }

        public void Request()
        {
            _state.Handle(this,ConsoleColor.White);
        }
    }
}
