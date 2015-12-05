using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
	class MessageDispatcher
	{
		List<Action> _pendingMsgs = new List<Action>(16);
		List<Action> _messages = new List<Action>(16);
		public void Post(Action msg)
		{
            _pendingMsgs.Add(msg);
		}

		public void Update()
		{
            _messages.Clear();
            // allow for reentrancy
            _messages.AddRange(_pendingMsgs);
            _pendingMsgs.Clear();
			foreach(Action it in _messages)
			{
				it();
			}
		}
	}
}