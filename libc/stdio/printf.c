#include <stdio.h>

/* This is a temporary hack until we can get a glibc's stdio working */

typedef __csharp__(System.Console) Console;
typedef __csharp__(System.IntPtr) IntPtr;
typedef __csharp__(System.Runtime.InteropServices.Marshal) Marshal;

int printf(const char *format, ...)
{
	va_list va;
	int posn;
	va_start(va, format);
	while(*format != '\0')
	{
		posn = 0;
		while(format[posn] != '\0' && format[posn] != '%')
		{
			++posn;
		}
		if(posn > 0)
		{
			__invoke__ Console.Write
				(__invoke__ Marshal.PtrToStringAnsi((IntPtr)format, posn));
			format += posn;
		}
		if(*format == '%')
		{
			++format;
			if(*format == 'd')
			{
				int val = va_arg(va, int);
				__invoke__ Console.Write(val);
				++format;
			}
			else if(*format == 'u')
			{
				unsigned int val = va_arg(va, unsigned int);
				__invoke__ Console.Write(val);
				++format;
			}
			else if(*format == 'l' && format[1] == 'd')
			{
				long val = va_arg(va, long);
				__invoke__ Console.Write(val);
				format += 2;
			}
			else if(*format == 'l' && format[1] == 'u')
			{
				unsigned long val = va_arg(va, unsigned long);
				__invoke__ Console.Write(val);
				format += 2;
			}
			else if(*format == 's')
			{
				const char *str = va_arg(va, const char *);
				if(!str)
				{
					str = "(null)";
				}
				__invoke__ Console.Write
					(__invoke__ Marshal.PtrToStringAnsi((IntPtr)str));
				++format;
			}
			else if(*format == '%')
			{
				__invoke__ Console.Write((__wchar__)'%');
				++format;
			}
		}
	}
	va_end(va);
	return 0;
}
