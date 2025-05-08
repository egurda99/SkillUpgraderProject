/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Contexts
{
	public static class InputAPI
	{
		///Keys
		public const int KeyboardInput = 1; // KeyboardInput


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static KeyboardInput GetKeyboardInput(this IContext obj) => obj.ResolveValue<KeyboardInput>(KeyboardInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetKeyboardInput(this IContext obj, out KeyboardInput value) => obj.TryResolveValue(KeyboardInput, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddKeyboardInput(this IContext obj, KeyboardInput value) => obj.AddValue(KeyboardInput, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelKeyboardInput(this IContext obj) => obj.DelValue(KeyboardInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetKeyboardInput(this IContext obj, KeyboardInput value) => obj.SetValue(KeyboardInput, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasKeyboardInput(this IContext obj) => obj.HasValue(KeyboardInput);
    }
}
