.assembly extern deeplingolib
{
  .ver 0:0:0:0
}
.assembly extern mscorlib
{
  .ver 4:0:0:0
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 ) // .z\V.4..
}
.assembly 'wow'
{
  .custom instance void class [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::'.ctor'() =  (
		01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
		63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01       ) // ceptionThrows.

  .hash algorithm 0x00008004
  .ver  0:0:0:0
}
.module wow.exe // GUID = {C7C9FEC5-F85A-44F2-94EE-C0B374145F1E}


  .class private auto ansi beforefieldinit CallDll
  	extends [mscorlib]System.Object
  {

    // method line 1
    .method public hidebysig specialname rtspecialname 
           instance default void '.ctor' ()  cil managed 
    {
        // Method begins at RVA 0x2050
	// Code size 7 (0x7)
	.maxstack 8
	IL_0000:  ldarg.0 
	IL_0001:  call instance void object::'.ctor'()
	IL_0006:  ret 
    } // end of method CallDll::.ctor

    // method line 2
    .method private static hidebysig 
           default int32 Wow (int32 a, int32 b)  cil managed 
    {
        // Method begins at RVA 0x2058
	// Code size 4 (0x4)
	.maxstack 8
	IL_0000:  ldarg.0 
	IL_0001:  ldarg.1 
	IL_0002:  add 
	IL_0003:  ret 
    } // end of method CallDll::Wow

    // method line 3
    .method public static hidebysig 
           default void Main ()  cil managed 
    {
        // Method begins at RVA 0x205d
	.entrypoint
	// Code size 29 (0x1d)
	.maxstack 8
	IL_0000:  ldc.i4.s 0x0a
	IL_0002:  call int32 class [deeplingolib]DeepLingo.Utils::New(int32)
	IL_0007:  pop 
	IL_0008:  ldc.i4.s 0x2a
	IL_000a:  call int32 class [deeplingolib]DeepLingo.Utils::Printi(int32)
	IL_000f:  pop 
	IL_0010:  ldc.i4.2 
	IL_0011:  ldc.i4.3 
	IL_0012:  call int32 class CallDll::Wow(int32, int32)
	IL_0017:  call void class [mscorlib]System.Console::WriteLine(int32)
	IL_001c:  ret 
    } // end of method CallDll::Main

  } // end of class CallDll

