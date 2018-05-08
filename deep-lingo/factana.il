.assembly extern mscorlib
{
  .ver 0:0:0:0
}
.assembly extern deeplingolib
{
  .ver 0:0:0:0
}
.assembly 'deeplingo'
{
  .hash algorithm 0x00000000
  .ver  0:0:0:0
}
.module factorial.exe // GUID = {6FADD1DB-6A5E-482B-ACF5-5450E1D33BA4}


  .class public auto ansi beforefieldinit DeepLingoProgram
  	extends [mscorlib]System.Object
  {

    // method line 1
    .method private static 
           default int32 iterative_factorial (int32 n)  cil managed 
    {
        // Method begins at RVA 0x2050
	// Code size 107 (0x6b)
	.maxstack 8
	.locals init (
		int32	V_0,
		int32	V_1)
	IL_0000:  ldc.i4 1
	IL_0005:  stloc 0

	IL_0009:  ldc.i4 2
	IL_000e:  stloc 1

	IL_0012:  ldloc 1

	IL_0016:  ldarg 0

	IL_001a:  ble.un IL_0025

	IL_001f:  ldc.i4.0 
	IL_0020:  br IL_002a

	IL_0025:  ldc.i4 42
	IL_002a:  ldc.i4 42
	IL_002f:  bne.un IL_003e

	IL_0034:  br IL_003e

	IL_0039:  br IL_003e

	IL_003e:  ldloc 0

	IL_0042:  ldloc 1

	IL_0046:  mul.ovf 
	IL_0047:  stloc 0

	IL_004b:  ldc.i4.1 
	IL_004c:  add.ovf 
	IL_004d:  stloc 1

	IL_0051:  br IL_0012

	IL_0056:  ldloc 0

	IL_005a:  ldloc 1

	IL_005e:  mul.ovf 
	IL_005f:  stloc 0

	IL_0063:  ldc.i4.1 
	IL_0064:  add.ovf 
	IL_0065:  stloc 1

	IL_0069:  ldc.i4.0 
	IL_006a:  ret 
    } // end of method DeepLingoProgram::iterative_factorial

    // method line 2
    .method private static 
           default int32 recursive_factorial (int32 n)  cil managed 
    {
        // Method begins at RVA 0x20c8
	// Code size 42 (0x2a)
	.maxstack 8
	IL_0000:  ldarg 0

	IL_0004:  ldc.i4 0
	IL_0009:  bgt.un IL_0014

	IL_000e:  ldc.i4.0 
	IL_000f:  br IL_0019

	IL_0014:  ldc.i4 42
	IL_0019:  ldc.i4 42
	IL_001e:  bne.un IL_0028

	IL_0023:  br IL_0028

	IL_0028:  ldc.i4.0 
	IL_0029:  ret 
    } // end of method DeepLingoProgram::recursive_factorial

    // method line 3
    .method private static 
           default int32 main ()  cil managed 
    {
        // Method begins at RVA 0x20f4
	.entrypoint
	// Code size 517 (0x205)
	.maxstack 8
	.locals init (
		int32	V_0,
		int32	V_1)
	IL_0000:  ldstr "Input a number: "
	IL_0005:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_000a:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_000f:  pop 
	IL_0010:  call int32 class [deeplingolib]DeepLingo.Utils::Readi()
	IL_0015:  stloc 0

	IL_0019:  ldstr "Iterative factorial: "
	IL_001e:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_0023:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_0028:  pop 
	IL_0029:  ldloc 0

	IL_002d:  call int32 class DeepLingoProgram::iterative_factorial(int32)
	IL_0032:  call int32 class [deeplingolib]DeepLingo.Utils::Printi(int32)
	IL_0037:  pop 
	IL_0038:  call int32 class [deeplingolib]DeepLingo.Utils::Println()
	IL_003d:  pop 
	IL_003e:  ldstr "Recursive factorial: "
	IL_0043:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_0048:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_004d:  pop 
	IL_004e:  ldloc 0

	IL_0052:  call int32 class DeepLingoProgram::recursive_factorial(int32)
	IL_0057:  call int32 class [deeplingolib]DeepLingo.Utils::Printi(int32)
	IL_005c:  pop 
	IL_005d:  call int32 class [deeplingolib]DeepLingo.Utils::Println()
	IL_0062:  pop 
	IL_0063:  ldstr "Compute another factorial? "
	IL_0068:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_006d:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_0072:  pop 
	IL_0073:  call int32 class [deeplingolib]DeepLingo.Utils::Reads()
	IL_0078:  stloc 1

	IL_007c:  ldloc 1

	IL_0080:  call int32 class [deeplingolib]DeepLingo.Utils::Size(int32)
	IL_0085:  ldc.i4 0
	IL_008a:  bne.un IL_0095

	IL_008f:  ldc.i4.0 
	IL_0090:  br IL_009a

	IL_0095:  ldc.i4 42
	IL_009a:  ldc.i4 42
	IL_009f:  bne.un IL_00ad

	IL_00a4:  stloc 1

	IL_00a8:  br IL_00bf

	IL_00ad:  ldloc 1

	IL_00b1:  ldc.i4 0
	IL_00b6:  call int32 class [deeplingolib]DeepLingo.Utils::Get(int32, int32)
	IL_00bb:  stloc 1

	IL_00bf:  ldloc 1

	IL_00c3:  beq.s IL_00cb

	IL_00c5:  ldc.i4.0 
	IL_00c6:  br IL_00d0

	IL_00cb:  ldc.i4 42
	IL_00d0:  ldloc 1

	IL_00d4:  ldc.i4 42
	IL_00d9:  beq.s IL_00e1

	IL_00db:  ldc.i4.0 
	IL_00dc:  br IL_00e6

	IL_00e1:  ldc.i4 42
	IL_00e6:  beq.s IL_00ee

	IL_00e8:  ldc.i4.0 
	IL_00e9:  br IL_00f3

	IL_00ee:  ldc.i4 42
	IL_00f3:  ldc.i4 42
	IL_00f8:  bne.un IL_0107

	IL_00fd:  br IL_0107

	IL_0102:  br IL_0107

	IL_0107:  br IL_0000

	IL_010c:  call int32 class [deeplingolib]DeepLingo.Utils::Readi()
	IL_0111:  stloc 0

	IL_0115:  ldstr "Iterative factorial: "
	IL_011a:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_011f:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_0124:  pop 
	IL_0125:  ldloc 0

	IL_0129:  call int32 class DeepLingoProgram::iterative_factorial(int32)
	IL_012e:  call int32 class [deeplingolib]DeepLingo.Utils::Printi(int32)
	IL_0133:  pop 
	IL_0134:  call int32 class [deeplingolib]DeepLingo.Utils::Println()
	IL_0139:  pop 
	IL_013a:  ldstr "Recursive factorial: "
	IL_013f:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_0144:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_0149:  pop 
	IL_014a:  ldloc 0

	IL_014e:  call int32 class DeepLingoProgram::recursive_factorial(int32)
	IL_0153:  call int32 class [deeplingolib]DeepLingo.Utils::Printi(int32)
	IL_0158:  pop 
	IL_0159:  call int32 class [deeplingolib]DeepLingo.Utils::Println()
	IL_015e:  pop 
	IL_015f:  ldstr "Compute another factorial? "
	IL_0164:  call int32 class [deeplingolib]DeepLingo.Utils::Conv(string)
	IL_0169:  call int32 class [deeplingolib]DeepLingo.Utils::Prints(int32)
	IL_016e:  pop 
	IL_016f:  call int32 class [deeplingolib]DeepLingo.Utils::Reads()
	IL_0174:  stloc 1

	IL_0178:  ldloc 1

	IL_017c:  call int32 class [deeplingolib]DeepLingo.Utils::Size(int32)
	IL_0181:  ldc.i4 0
	IL_0186:  bne.un IL_0191

	IL_018b:  ldc.i4.0 
	IL_018c:  br IL_0196

	IL_0191:  ldc.i4 42
	IL_0196:  ldc.i4 42
	IL_019b:  bne.un IL_01a9

	IL_01a0:  stloc 1

	IL_01a4:  br IL_01bb

	IL_01a9:  ldloc 1

	IL_01ad:  ldc.i4 0
	IL_01b2:  call int32 class [deeplingolib]DeepLingo.Utils::Get(int32, int32)
	IL_01b7:  stloc 1

	IL_01bb:  ldloc 1

	IL_01bf:  beq.s IL_01c7

	IL_01c1:  ldc.i4.0 
	IL_01c2:  br IL_01cc

	IL_01c7:  ldc.i4 42
	IL_01cc:  ldloc 1

	IL_01d0:  ldc.i4 42
	IL_01d5:  beq.s IL_01dd

	IL_01d7:  ldc.i4.0 
	IL_01d8:  br IL_01e2

	IL_01dd:  ldc.i4 42
	IL_01e2:  beq.s IL_01ea

	IL_01e4:  ldc.i4.0 
	IL_01e5:  br IL_01ef

	IL_01ea:  ldc.i4 42
	IL_01ef:  ldc.i4 42
	IL_01f4:  bne.un IL_0203

	IL_01f9:  br IL_0203

	IL_01fe:  br IL_0203

	IL_0203:  ldc.i4.0 
	IL_0204:  ret 
    } // end of method DeepLingoProgram::main

  } // end of class DeepLingoProgram

