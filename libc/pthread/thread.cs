/*
 * thread.cs - Access to C# thread operations.
 *
 * This file is part of the Portable.NET C library.
 * Copyright (C) 2004  Southern Storm Software, Pty Ltd.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections;
using System.Threading;
using OpenSystem.C;

namespace OpenSystem.C
{

// Class that keeps track of the thread identifiers in the system.
internal class ThreadInfo
{
	// Internal state.
	public static Hashtable threads = new Hashtable();
	public static Hashtable states  = new Hashtable();
	public static long nextId = 1;

}; // class ThreadInfo

}; // namespace OpenSystem.C

__module
{

	// Register a thread that was created by "pthread_create".
	public static long __libc_thread_register(Thread thread, Object state)
			{
				lock(typeof(ThreadInfo))
				{
					long id = (ThreadInfo.nextId)++;
					ThreadInfo.threads[id] = thread;
					ThreadInfo.states[id] = state;
					return id;
				}
			}

	// Register a thread that was created by a foreign process.
	public static long __libc_thread_register_foreign(Thread thread)
			{
				lock(typeof(ThreadInfo))
				{
					long id = (ThreadInfo.nextId)++;
					ThreadInfo.threads[id] = thread;
					return id;
				}
			}

	// Unregister a thread.
	public static void __libc_thread_unregister(long id)
			{
				lock(typeof(ThreadInfo))
				{
					ThreadInfo.threads.Remove(id);
					ThreadInfo.states.Remove(id);
				}
			}

	// Get the thread object associated with a thread identifier.
	public static Thread __libc_thread_object(long id)
			{
				lock(typeof(ThreadInfo))
				{
					return (Thread)(ThreadInfo.threads[id]);
				}
			}

	// Get the state object associated with a thread identifier.
	public static Object __libc_thread_state(long id)
			{
				lock(typeof(ThreadInfo))
				{
					return ThreadInfo.states[id];
				}
			}

} // __module
