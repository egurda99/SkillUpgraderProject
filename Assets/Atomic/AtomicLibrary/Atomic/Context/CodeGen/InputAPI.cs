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
		public const int MouseInput = 2; // MouseInput


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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MouseInput GetMouseInput(this IContext obj) => obj.ResolveValue<MouseInput>(MouseInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMouseInput(this IContext obj, out MouseInput value) => obj.TryResolveValue(MouseInput, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMouseInput(this IContext obj, MouseInput value) => obj.AddValue(MouseInput, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMouseInput(this IContext obj) => obj.DelValue(MouseInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMouseInput(this IContext obj, MouseInput value) => obj.SetValue(MouseInput, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMouseInput(this IContext obj) => obj.HasValue(MouseInput);
    }
}
