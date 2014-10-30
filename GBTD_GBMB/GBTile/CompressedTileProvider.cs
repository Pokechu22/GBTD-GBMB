using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.Tile
{
	struct GBRegisters
	{
		public byte A, B, C, D, E, F, H, L;
		public UInt16 AF {
			get {
				return (UInt16)((A << 8) | F);
			}
			set {
				A = (byte)((value >> 8) & 0xFF);
				F = (byte)((value) & 0xFF);
			}
		}
		public UInt16 BC {
			get {
				return (UInt16)((B << 8) | C);
			}
			set {
				B = (byte)((value >> 8) & 0xFF);
				C = (byte)((value) & 0xFF);
			}
		}
		public UInt16 DE {
			get {
				return (UInt16)((D << 8) | E);
			}
			set {
				D = (byte)((value >> 8) & 0xFF);
				E = (byte)((value) & 0xFF);
			}
		}
		public UInt16 HL {
			get {
				return (UInt16)((H << 8) | L);
			}
			set {
				H = (byte)((value >> 8) & 0xFF);
				L = (byte)((value) & 0xFF);
			}
		}
	}
	/*
	 * FULL ASM DUMP: 

	LoadGBCompressPatterns::
	;***********************************************************************
	;*                                                                     *
	;* This function will load a set of compressed tiles.                  *
	;* It is heavily based on the original decomp-routine by Jens Ch.      *
	;* Restemeier. As this routine is optimized for VRAM, use the original *
	;* routine to decompress data to normal RAM.                           *
	;*                                                                     *
	;* Input:                                                              *
	;*   hl = Source                                                       *
	;*   de = destination                                                  *
	;*                                                                     *
	;* To get correct data for this function, let GBTD export to:          *
	;*   Compression = Checked                                             *
	;*   Format      = Gameboy 4 color                                     *
	;*   Counter     = None (or as Constant)                               *
	;*                                                                     *
	;***********************************************************************

	  push hl
	  push de
	  push bc

	  di                            ; prevent any interruptions

	.NextCommand:
	  ld a,[hl+]                    ; load command
	  or a
	  jr z, .EndFound               ; exit, if last byte

		bit 7,a
		jr nz, .ds                  ; string functions

		bit 6,a
		jr nz, .dw1


		  ;* RLE byte *
		  and 63                    ; calc counter
		  inc a
		  ld b,a
		  ld a,[hl+]
		  ld c,a                    ; save a
	.db1:   WaitForVRAM
			ld a,c
			ld [de],a
		inc de
		  dec b
		  jr nz, .db1

		jr .NextCommand             ; next command


		  ;* RLE word *
	.dw1: and 63
		  inc a
		  ld c,a                    ; c = counter

		  ld b,[hl]                 ; load word into b-l
		  inc hl
		  ld a,[hl+]

		  push hl                   ; save hl for later

		  ld l,a

	.dw2:   WaitForVRAM
		ld a,b                  ; store word
		ld [de],a
		inc de
		ld a,l
		ld [de],a
		inc de

		  dec c
		  jr nz, .dw2

		pop hl

		jr .NextCommand             ; next command


	.ds:bit 6,a
		jr nz, .dc


		  ;* string repeat *
		  and 63
		  inc a
		  push hl
		  ld c,[hl]
		  inc hl
		  ld b,[hl]
		  ld h,d
		  ld l,e
		  add hl,bc
		  ld b,a

	.dr1:   WaitForVRAM
			ld a,[hl+]
			ld [de],a
			inc de
		  dec b
		  jr nz, .dr1

		  pop hl
		  inc hl
		  inc hl

		jr .NextCommand             ; next command


		  ;* string copy *
	.dc:  and 63
		  inc a
		  ld b,a

	.dc1:   WaitForVRAM
			ld a,[hl+]
			ld [de],a
			inc de
		  dec b
		  jr nz, .dc1

		jr .NextCommand             ; next command


	.EndFound:
	  ei                            ; enable interrupts again

	  pop bc
	  pop de
	  pop hl

	ret

	 * 
	 * From lines 52-186, GBTDLIB.Z80.  
	 * 
	 * Information about this is currently found on http://www.devrs.com/gb/hmgd/supp.html#GBTD%20Library,
	 * and a download is avaliable at http://www.devrs.com/gb/hmgd/gbtdlib1.zip.
	 */
	/// <summary>
	/// Loads a set of tiles compressed with the GB compression algoritm.
	/// 
	/// This algoritm was written in assembly, and I don't fully understand it.  
	/// </summary>
	public class CompressedTileProvider : TileProvider
	{
		private List<Tile> tiles = new List<Tile>();

		public CompressedTileProvider(Stream source) {
			//Read the entire buffer.
			Byte[] buffer = new Byte[source.Length];
			source.Read(buffer, 0, (int)source.Length);

			readFromBytes(buffer);
		}

		public CompressedTileProvider(Byte[] source) {
			readFromBytes(source);
		}

		/// <summary>
		/// Reads from a set of bytes.
		/// </summary>
		/// <param name="data">The data to load from (Would be pointed to by hl in origional)</param>
		protected internal void readFromBytes(Byte[] data) {
			byte[] mem = new byte[0x2000];

			//Goto is used here.  The origional algoritm uses goto, so it shall be used, regardless of the fact that it is a bad idea.
			//This is ported ASM.
			GBRegisters reg = new GBRegisters();

			int index = 0;

			UInt16 tempHL = 0x0;

		NextCommand:
			reg.A = data[index++];
			//or a?
			if (reg.A == 0) { //jz z, .EndFound    ; This means jump if [z]ero.
				goto EndFound;
			}

			//Test if bit 7 is set in a:
			//bit 7,a
			//jr nz, .ds
			if ((reg.A & 0x80) != 0) {
				goto ds;
			}
			if ((reg.A & 0x40) != 0) {
				goto dw1;
			}

			reg.A &= 0x3F;
			reg.A++;
			reg.B = reg.A;
			reg.A = data[index++];
			reg.C = reg.A;

		db1:
			//WaitForVRAM(); //N/A
			reg.A = reg.C;
			mem[reg.DE] = reg.A;
			reg.DE++;
			reg.B--;
			if (reg.A != 0) {
				goto db1;
			}

			goto NextCommand;

		dw1:
			reg.A &= 0x3F;
			reg.A++;
			reg.C = reg.A;

			reg.B = data[index];
			index++;
			reg.A = data[index++];

			tempHL = reg.HL;

			reg.L = reg.A;

		dw2:
			//Waitforvram.
			reg.A = reg.B;
			mem[reg.DE] = reg.A;
			reg.DE++;
			reg.A = reg.L;
			mem[reg.DE] = reg.A;
			reg.DE++;
			reg.C--;
			if (reg.A != 0) {
				goto dw2;
			}

			reg.HL = tempHL;

			goto NextCommand;

		ds:
			if ((reg.A & 0x40) != 0) {
				goto dc;
			}

			//String repeat (?)
			reg.A &= 0x3F;
			reg.A++;
			tempHL = reg.HL;
			reg.C = mem[reg.HL];
			reg.HL++;
			reg.B = mem[reg.HL];
			reg.H = reg.E;
			reg.L = reg.E;
			reg.HL += reg.BC;
			reg.B = reg.A;

		dr1:
			//WaitForVRAM();
			reg.A = mem[reg.HL++];
			mem[reg.DE] = reg.A;
			reg.DE++;
			reg.B--;
			if (reg.A != 0) {
				goto dr1;
			}
			reg.HL = tempHL;
			reg.HL++;
			reg.HL++;

			goto NextCommand;
		dc:
			reg.A &= 0x3F;
			reg.A++;
			reg.B = reg.A;

		dc1:
			//WaitForVRAM
			reg.A = mem[reg.HL++];
			mem[reg.DE] = reg.A;
			reg.DE++;
			if (reg.A != 0) {
				goto dc1;
			}
			goto NextCommand;
		EndFound:
			//Take stuff at reg.de out.
			byte[] usedMem = new byte[reg.DE];
			Array.Copy(mem, usedMem, reg.DE);

			VRAMTileProvider vprov = new VRAMTileProvider(usedMem);
			this.tiles = vprov.getTiles();

			return;
		}

		public List<Tile> getTiles() {
			return new List<Tile>(tiles);
		}

		public int getNumberOfTiles() {
			throw new NotImplementedException();
		}
	}
}
