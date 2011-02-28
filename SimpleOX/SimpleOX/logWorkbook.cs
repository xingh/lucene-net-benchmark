﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Collections;

using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;

namespace SimpleOX.Excel {
	public class LogWorkbook {

		private LinkedList<String[]> _sharedStrings = new LinkedList<String[]>();

		public void addSharedString(String cell, String sharedString ) {
			String[] entry = new String[2] { cell, sharedString };
			_sharedStrings.AddLast( entry );
		}

		// Creates a SpreadsheetDocument.
		public void CreatePackage( string filePath ) {
			using( SpreadsheetDocument package = SpreadsheetDocument.Create( filePath, SpreadsheetDocumentType.Workbook ) ) {
				CreateParts( package );
			}
		}

		// Adds child parts and generates content of the specified part.
		private void CreateParts( SpreadsheetDocument document ) {
			ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>( "rId3" );
			GenerateExtendedFilePropertiesPart1Content( extendedFilePropertiesPart1 );

			WorkbookPart workbookPart1 = document.AddWorkbookPart();
			GenerateWorkbookPart1Content( workbookPart1 );

			WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>( "rId3" );
			GenerateWorkbookStylesPart1Content( workbookStylesPart1 );

			ThemePart themePart1 = workbookPart1.AddNewPart<ThemePart>( "rId2" );
			GenerateThemePart1Content( themePart1 );

			WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>( "rId1" );
			GenerateWorksheetPart1Content( worksheetPart1 );

			WorksheetPart worksheetPart2 = workbookPart1.AddNewPart<WorksheetPart>( "rId5" );
			GenerateWorksheetPart1Content( worksheetPart2 );

			SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1 = worksheetPart1.AddNewPart<SpreadsheetPrinterSettingsPart>( "rId1" );
			GenerateSpreadsheetPrinterSettingsPart1Content( spreadsheetPrinterSettingsPart1 );

			SharedStringTablePart sharedStringTablePart1 = workbookPart1.AddNewPart<SharedStringTablePart>( "rId4" );
			GenerateSharedStringTablePart1Content( sharedStringTablePart1 );

			SetPackageProperties( document );
		}

		// Generates content of extendedFilePropertiesPart1.
		public void GenerateExtendedFilePropertiesPart1Content( ExtendedFilePropertiesPart extendedFilePropertiesPart1 ) {
			Ap.Properties properties1 = new Ap.Properties();
			properties1.AddNamespaceDeclaration( "vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes" );
			Ap.Application application1 = new Ap.Application();
			application1.Text = "Microsoft Excel";
			Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
			documentSecurity1.Text = "0";
			Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
			scaleCrop1.Text = "false";

			Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

			Vt.VTVector vTVector1 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

			Vt.Variant variant1 = new Vt.Variant();
			Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
			vTLPSTR1.Text = "Worksheets";

			variant1.Append( vTLPSTR1 );

			Vt.Variant variant2 = new Vt.Variant();
			Vt.VTInt32 vTInt321 = new Vt.VTInt32();
			vTInt321.Text = "2"; // The number of sheets their are

			variant2.Append( vTInt321 );

			vTVector1.Append( variant1 );
			vTVector1.Append( variant2 );

			headingPairs1.Append( vTVector1 );

			Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

			Vt.VTVector vTVector2 = new Vt.VTVector() { BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)2U }; // size needs to be the number of sheets
			Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
			vTLPSTR2.Text = "Log";
			Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR(); // ad now vector for sheet titles
			vTLPSTR3.Text = "Sheet1";

			vTVector2.Append( vTLPSTR2 );
			vTVector2.Append( vTLPSTR3 ); // append each of them

			titlesOfParts1.Append( vTVector2 );
			Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
			linksUpToDate1.Text = "false";
			Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
			sharedDocument1.Text = "false";
			Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
			hyperlinksChanged1.Text = "false";
			Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
			applicationVersion1.Text = "12.0000";

			properties1.Append( application1 );
			properties1.Append( documentSecurity1 );
			properties1.Append( scaleCrop1 );
			properties1.Append( headingPairs1 );
			properties1.Append( titlesOfParts1 );
			properties1.Append( linksUpToDate1 );
			properties1.Append( sharedDocument1 );
			properties1.Append( hyperlinksChanged1 );
			properties1.Append( applicationVersion1 );

			extendedFilePropertiesPart1.Properties = properties1;
		}

		// Generates content of workbookPart1.
		public void GenerateWorkbookPart1Content( WorkbookPart workbookPart1 ) {
			Workbook workbook1 = new Workbook();
			workbook1.AddNamespaceDeclaration( "r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships" );
			FileVersion fileVersion1 = new FileVersion() { ApplicationName = "xl", LastEdited = "4", LowestEdited = "4", BuildVersion = "4506" };
			WorkbookProperties workbookProperties1 = new WorkbookProperties() { DefaultThemeVersion = (UInt32Value)124226U };

			BookViews bookViews1 = new BookViews();
			WorkbookView workbookView1 = new WorkbookView() { XWindow = 0, YWindow = 90, WindowWidth = (UInt32Value)28755U, WindowHeight = (UInt32Value)12585U };

			bookViews1.Append( workbookView1 );

			Sheets sheets1 = new Sheets();
			Sheet sheet1 = new Sheet() { Name = "Log", SheetId = (UInt32Value)1U, Id = "rId1" };
			Sheet sheet2 = new Sheet() { Name = "Sheet1", SheetId = (UInt32Value)2U, Id = "rId5" }; // create each sheet with it's title and a unique id

			sheets1.Append( sheet1 );
			sheets1.Append( sheet2 ); // then append the sheets
			CalculationProperties calculationProperties1 = new CalculationProperties() { CalculationId = (UInt32Value)125725U };

			workbook1.Append( fileVersion1 );
			workbook1.Append( workbookProperties1 );
			workbook1.Append( bookViews1 );
			workbook1.Append( sheets1 );
			workbook1.Append( calculationProperties1 );

			workbookPart1.Workbook = workbook1;
		}

		// Generates content of workbookStylesPart1.
		public void GenerateWorkbookStylesPart1Content( WorkbookStylesPart workbookStylesPart1 ) {
			Stylesheet stylesheet1 = new Stylesheet();

			Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U };

			Font font1 = new Font();
			FontSize fontSize1 = new FontSize() { Val = 11D };
			Color color1 = new Color() { Theme = (UInt32Value)1U };
			FontName fontName1 = new FontName() { Val = "Calibri" };
			FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
			FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

			font1.Append( fontSize1 );
			font1.Append( color1 );
			font1.Append( fontName1 );
			font1.Append( fontFamilyNumbering1 );
			font1.Append( fontScheme1 );

			fonts1.Append( font1 );

			Fills fills1 = new Fills() { Count = (UInt32Value)2U };

			Fill fill1 = new Fill();
			PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };

			fill1.Append( patternFill1 );

			Fill fill2 = new Fill();
			PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };

			fill2.Append( patternFill2 );

			fills1.Append( fill1 );
			fills1.Append( fill2 );

			Borders borders1 = new Borders() { Count = (UInt32Value)1U };

			Border border1 = new Border();
			LeftBorder leftBorder1 = new LeftBorder();
			RightBorder rightBorder1 = new RightBorder();
			TopBorder topBorder1 = new TopBorder();
			BottomBorder bottomBorder1 = new BottomBorder();
			DiagonalBorder diagonalBorder1 = new DiagonalBorder();

			border1.Append( leftBorder1 );
			border1.Append( rightBorder1 );
			border1.Append( topBorder1 );
			border1.Append( bottomBorder1 );
			border1.Append( diagonalBorder1 );

			borders1.Append( border1 );

			CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
			CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

			cellStyleFormats1.Append( cellFormat1 );

			CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)2U };
			CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };

			CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyAlignment = true };
			Alignment alignment1 = new Alignment() { WrapText = true };

			cellFormat3.Append( alignment1 );

			cellFormats1.Append( cellFormat2 );
			cellFormats1.Append( cellFormat3 );

			CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
			CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

			cellStyles1.Append( cellStyle1 );
			DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
			TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium9", DefaultPivotStyle = "PivotStyleLight16" };

			stylesheet1.Append( fonts1 );
			stylesheet1.Append( fills1 );
			stylesheet1.Append( borders1 );
			stylesheet1.Append( cellStyleFormats1 );
			stylesheet1.Append( cellFormats1 );
			stylesheet1.Append( cellStyles1 );
			stylesheet1.Append( differentialFormats1 );
			stylesheet1.Append( tableStyles1 );

			workbookStylesPart1.Stylesheet = stylesheet1;
		}

		// Generates content of themePart1.
		public void GenerateThemePart1Content( ThemePart themePart1 ) {
			A.Theme theme1 = new A.Theme() { Name = "Office Theme" };
			theme1.AddNamespaceDeclaration( "a", "http://schemas.openxmlformats.org/drawingml/2006/main" );

			A.ThemeElements themeElements1 = new A.ThemeElements();

			A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Office" };

			A.Dark1Color dark1Color1 = new A.Dark1Color();
			A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

			dark1Color1.Append( systemColor1 );

			A.Light1Color light1Color1 = new A.Light1Color();
			A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

			light1Color1.Append( systemColor2 );

			A.Dark2Color dark2Color1 = new A.Dark2Color();
			A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "1F497D" };

			dark2Color1.Append( rgbColorModelHex1 );

			A.Light2Color light2Color1 = new A.Light2Color();
			A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "EEECE1" };

			light2Color1.Append( rgbColorModelHex2 );

			A.Accent1Color accent1Color1 = new A.Accent1Color();
			A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "4F81BD" };

			accent1Color1.Append( rgbColorModelHex3 );

			A.Accent2Color accent2Color1 = new A.Accent2Color();
			A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "C0504D" };

			accent2Color1.Append( rgbColorModelHex4 );

			A.Accent3Color accent3Color1 = new A.Accent3Color();
			A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "9BBB59" };

			accent3Color1.Append( rgbColorModelHex5 );

			A.Accent4Color accent4Color1 = new A.Accent4Color();
			A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "8064A2" };

			accent4Color1.Append( rgbColorModelHex6 );

			A.Accent5Color accent5Color1 = new A.Accent5Color();
			A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "4BACC6" };

			accent5Color1.Append( rgbColorModelHex7 );

			A.Accent6Color accent6Color1 = new A.Accent6Color();
			A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "F79646" };

			accent6Color1.Append( rgbColorModelHex8 );

			A.Hyperlink hyperlink1 = new A.Hyperlink();
			A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "0000FF" };

			hyperlink1.Append( rgbColorModelHex9 );

			A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
			A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "800080" };

			followedHyperlinkColor1.Append( rgbColorModelHex10 );

			colorScheme1.Append( dark1Color1 );
			colorScheme1.Append( light1Color1 );
			colorScheme1.Append( dark2Color1 );
			colorScheme1.Append( light2Color1 );
			colorScheme1.Append( accent1Color1 );
			colorScheme1.Append( accent2Color1 );
			colorScheme1.Append( accent3Color1 );
			colorScheme1.Append( accent4Color1 );
			colorScheme1.Append( accent5Color1 );
			colorScheme1.Append( accent6Color1 );
			colorScheme1.Append( hyperlink1 );
			colorScheme1.Append( followedHyperlinkColor1 );

			A.FontScheme fontScheme2 = new A.FontScheme() { Name = "Office" };

			A.MajorFont majorFont1 = new A.MajorFont();
			A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Cambria" };
			A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
			A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };
			A.SupplementalFont supplementalFont1 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
			A.SupplementalFont supplementalFont2 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
			A.SupplementalFont supplementalFont3 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
			A.SupplementalFont supplementalFont4 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
			A.SupplementalFont supplementalFont5 = new A.SupplementalFont() { Script = "Arab", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont6 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont7 = new A.SupplementalFont() { Script = "Thai", Typeface = "Tahoma" };
			A.SupplementalFont supplementalFont8 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
			A.SupplementalFont supplementalFont9 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
			A.SupplementalFont supplementalFont10 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
			A.SupplementalFont supplementalFont11 = new A.SupplementalFont() { Script = "Khmr", Typeface = "MoolBoran" };
			A.SupplementalFont supplementalFont12 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
			A.SupplementalFont supplementalFont13 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
			A.SupplementalFont supplementalFont14 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
			A.SupplementalFont supplementalFont15 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
			A.SupplementalFont supplementalFont16 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
			A.SupplementalFont supplementalFont17 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
			A.SupplementalFont supplementalFont18 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
			A.SupplementalFont supplementalFont19 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
			A.SupplementalFont supplementalFont20 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
			A.SupplementalFont supplementalFont21 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
			A.SupplementalFont supplementalFont22 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
			A.SupplementalFont supplementalFont23 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
			A.SupplementalFont supplementalFont24 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
			A.SupplementalFont supplementalFont25 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
			A.SupplementalFont supplementalFont26 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
			A.SupplementalFont supplementalFont27 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
			A.SupplementalFont supplementalFont28 = new A.SupplementalFont() { Script = "Viet", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont29 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };

			majorFont1.Append( latinFont1 );
			majorFont1.Append( eastAsianFont1 );
			majorFont1.Append( complexScriptFont1 );
			majorFont1.Append( supplementalFont1 );
			majorFont1.Append( supplementalFont2 );
			majorFont1.Append( supplementalFont3 );
			majorFont1.Append( supplementalFont4 );
			majorFont1.Append( supplementalFont5 );
			majorFont1.Append( supplementalFont6 );
			majorFont1.Append( supplementalFont7 );
			majorFont1.Append( supplementalFont8 );
			majorFont1.Append( supplementalFont9 );
			majorFont1.Append( supplementalFont10 );
			majorFont1.Append( supplementalFont11 );
			majorFont1.Append( supplementalFont12 );
			majorFont1.Append( supplementalFont13 );
			majorFont1.Append( supplementalFont14 );
			majorFont1.Append( supplementalFont15 );
			majorFont1.Append( supplementalFont16 );
			majorFont1.Append( supplementalFont17 );
			majorFont1.Append( supplementalFont18 );
			majorFont1.Append( supplementalFont19 );
			majorFont1.Append( supplementalFont20 );
			majorFont1.Append( supplementalFont21 );
			majorFont1.Append( supplementalFont22 );
			majorFont1.Append( supplementalFont23 );
			majorFont1.Append( supplementalFont24 );
			majorFont1.Append( supplementalFont25 );
			majorFont1.Append( supplementalFont26 );
			majorFont1.Append( supplementalFont27 );
			majorFont1.Append( supplementalFont28 );
			majorFont1.Append( supplementalFont29 );

			A.MinorFont minorFont1 = new A.MinorFont();
			A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri" };
			A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
			A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };
			A.SupplementalFont supplementalFont30 = new A.SupplementalFont() { Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
			A.SupplementalFont supplementalFont31 = new A.SupplementalFont() { Script = "Hang", Typeface = "맑은 고딕" };
			A.SupplementalFont supplementalFont32 = new A.SupplementalFont() { Script = "Hans", Typeface = "宋体" };
			A.SupplementalFont supplementalFont33 = new A.SupplementalFont() { Script = "Hant", Typeface = "新細明體" };
			A.SupplementalFont supplementalFont34 = new A.SupplementalFont() { Script = "Arab", Typeface = "Arial" };
			A.SupplementalFont supplementalFont35 = new A.SupplementalFont() { Script = "Hebr", Typeface = "Arial" };
			A.SupplementalFont supplementalFont36 = new A.SupplementalFont() { Script = "Thai", Typeface = "Tahoma" };
			A.SupplementalFont supplementalFont37 = new A.SupplementalFont() { Script = "Ethi", Typeface = "Nyala" };
			A.SupplementalFont supplementalFont38 = new A.SupplementalFont() { Script = "Beng", Typeface = "Vrinda" };
			A.SupplementalFont supplementalFont39 = new A.SupplementalFont() { Script = "Gujr", Typeface = "Shruti" };
			A.SupplementalFont supplementalFont40 = new A.SupplementalFont() { Script = "Khmr", Typeface = "DaunPenh" };
			A.SupplementalFont supplementalFont41 = new A.SupplementalFont() { Script = "Knda", Typeface = "Tunga" };
			A.SupplementalFont supplementalFont42 = new A.SupplementalFont() { Script = "Guru", Typeface = "Raavi" };
			A.SupplementalFont supplementalFont43 = new A.SupplementalFont() { Script = "Cans", Typeface = "Euphemia" };
			A.SupplementalFont supplementalFont44 = new A.SupplementalFont() { Script = "Cher", Typeface = "Plantagenet Cherokee" };
			A.SupplementalFont supplementalFont45 = new A.SupplementalFont() { Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
			A.SupplementalFont supplementalFont46 = new A.SupplementalFont() { Script = "Tibt", Typeface = "Microsoft Himalaya" };
			A.SupplementalFont supplementalFont47 = new A.SupplementalFont() { Script = "Thaa", Typeface = "MV Boli" };
			A.SupplementalFont supplementalFont48 = new A.SupplementalFont() { Script = "Deva", Typeface = "Mangal" };
			A.SupplementalFont supplementalFont49 = new A.SupplementalFont() { Script = "Telu", Typeface = "Gautami" };
			A.SupplementalFont supplementalFont50 = new A.SupplementalFont() { Script = "Taml", Typeface = "Latha" };
			A.SupplementalFont supplementalFont51 = new A.SupplementalFont() { Script = "Syrc", Typeface = "Estrangelo Edessa" };
			A.SupplementalFont supplementalFont52 = new A.SupplementalFont() { Script = "Orya", Typeface = "Kalinga" };
			A.SupplementalFont supplementalFont53 = new A.SupplementalFont() { Script = "Mlym", Typeface = "Kartika" };
			A.SupplementalFont supplementalFont54 = new A.SupplementalFont() { Script = "Laoo", Typeface = "DokChampa" };
			A.SupplementalFont supplementalFont55 = new A.SupplementalFont() { Script = "Sinh", Typeface = "Iskoola Pota" };
			A.SupplementalFont supplementalFont56 = new A.SupplementalFont() { Script = "Mong", Typeface = "Mongolian Baiti" };
			A.SupplementalFont supplementalFont57 = new A.SupplementalFont() { Script = "Viet", Typeface = "Arial" };
			A.SupplementalFont supplementalFont58 = new A.SupplementalFont() { Script = "Uigh", Typeface = "Microsoft Uighur" };

			minorFont1.Append( latinFont2 );
			minorFont1.Append( eastAsianFont2 );
			minorFont1.Append( complexScriptFont2 );
			minorFont1.Append( supplementalFont30 );
			minorFont1.Append( supplementalFont31 );
			minorFont1.Append( supplementalFont32 );
			minorFont1.Append( supplementalFont33 );
			minorFont1.Append( supplementalFont34 );
			minorFont1.Append( supplementalFont35 );
			minorFont1.Append( supplementalFont36 );
			minorFont1.Append( supplementalFont37 );
			minorFont1.Append( supplementalFont38 );
			minorFont1.Append( supplementalFont39 );
			minorFont1.Append( supplementalFont40 );
			minorFont1.Append( supplementalFont41 );
			minorFont1.Append( supplementalFont42 );
			minorFont1.Append( supplementalFont43 );
			minorFont1.Append( supplementalFont44 );
			minorFont1.Append( supplementalFont45 );
			minorFont1.Append( supplementalFont46 );
			minorFont1.Append( supplementalFont47 );
			minorFont1.Append( supplementalFont48 );
			minorFont1.Append( supplementalFont49 );
			minorFont1.Append( supplementalFont50 );
			minorFont1.Append( supplementalFont51 );
			minorFont1.Append( supplementalFont52 );
			minorFont1.Append( supplementalFont53 );
			minorFont1.Append( supplementalFont54 );
			minorFont1.Append( supplementalFont55 );
			minorFont1.Append( supplementalFont56 );
			minorFont1.Append( supplementalFont57 );
			minorFont1.Append( supplementalFont58 );

			fontScheme2.Append( majorFont1 );
			fontScheme2.Append( minorFont1 );

			A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Office" };

			A.FillStyleList fillStyleList1 = new A.FillStyleList();

			A.SolidFill solidFill1 = new A.SolidFill();
			A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

			solidFill1.Append( schemeColor1 );

			A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

			A.GradientStopList gradientStopList1 = new A.GradientStopList();

			A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

			A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Tint tint1 = new A.Tint() { Val = 50000 };
			A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 300000 };

			schemeColor2.Append( tint1 );
			schemeColor2.Append( saturationModulation1 );

			gradientStop1.Append( schemeColor2 );

			A.GradientStop gradientStop2 = new A.GradientStop() { Position = 35000 };

			A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Tint tint2 = new A.Tint() { Val = 37000 };
			A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 300000 };

			schemeColor3.Append( tint2 );
			schemeColor3.Append( saturationModulation2 );

			gradientStop2.Append( schemeColor3 );

			A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

			A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Tint tint3 = new A.Tint() { Val = 15000 };
			A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 350000 };

			schemeColor4.Append( tint3 );
			schemeColor4.Append( saturationModulation3 );

			gradientStop3.Append( schemeColor4 );

			gradientStopList1.Append( gradientStop1 );
			gradientStopList1.Append( gradientStop2 );
			gradientStopList1.Append( gradientStop3 );
			A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 16200000, Scaled = true };

			gradientFill1.Append( gradientStopList1 );
			gradientFill1.Append( linearGradientFill1 );

			A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

			A.GradientStopList gradientStopList2 = new A.GradientStopList();

			A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

			A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Shade shade1 = new A.Shade() { Val = 51000 };
			A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 130000 };

			schemeColor5.Append( shade1 );
			schemeColor5.Append( saturationModulation4 );

			gradientStop4.Append( schemeColor5 );

			A.GradientStop gradientStop5 = new A.GradientStop() { Position = 80000 };

			A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Shade shade2 = new A.Shade() { Val = 93000 };
			A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 130000 };

			schemeColor6.Append( shade2 );
			schemeColor6.Append( saturationModulation5 );

			gradientStop5.Append( schemeColor6 );

			A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

			A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Shade shade3 = new A.Shade() { Val = 94000 };
			A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 135000 };

			schemeColor7.Append( shade3 );
			schemeColor7.Append( saturationModulation6 );

			gradientStop6.Append( schemeColor7 );

			gradientStopList2.Append( gradientStop4 );
			gradientStopList2.Append( gradientStop5 );
			gradientStopList2.Append( gradientStop6 );
			A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 16200000, Scaled = false };

			gradientFill2.Append( gradientStopList2 );
			gradientFill2.Append( linearGradientFill2 );

			fillStyleList1.Append( solidFill1 );
			fillStyleList1.Append( gradientFill1 );
			fillStyleList1.Append( gradientFill2 );

			A.LineStyleList lineStyleList1 = new A.LineStyleList();

			A.Outline outline1 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill2 = new A.SolidFill();

			A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Shade shade4 = new A.Shade() { Val = 95000 };
			A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 105000 };

			schemeColor8.Append( shade4 );
			schemeColor8.Append( saturationModulation7 );

			solidFill2.Append( schemeColor8 );
			A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

			outline1.Append( solidFill2 );
			outline1.Append( presetDash1 );

			A.Outline outline2 = new A.Outline() { Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill3 = new A.SolidFill();
			A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

			solidFill3.Append( schemeColor9 );
			A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

			outline2.Append( solidFill3 );
			outline2.Append( presetDash2 );

			A.Outline outline3 = new A.Outline() { Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill4 = new A.SolidFill();
			A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

			solidFill4.Append( schemeColor10 );
			A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

			outline3.Append( solidFill4 );
			outline3.Append( presetDash3 );

			lineStyleList1.Append( outline1 );
			lineStyleList1.Append( outline2 );
			lineStyleList1.Append( outline3 );

			A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

			A.EffectStyle effectStyle1 = new A.EffectStyle();

			A.EffectList effectList1 = new A.EffectList();

			A.OuterShadow outerShadow1 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

			A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
			A.Alpha alpha1 = new A.Alpha() { Val = 38000 };

			rgbColorModelHex11.Append( alpha1 );

			outerShadow1.Append( rgbColorModelHex11 );

			effectList1.Append( outerShadow1 );

			effectStyle1.Append( effectList1 );

			A.EffectStyle effectStyle2 = new A.EffectStyle();

			A.EffectList effectList2 = new A.EffectList();

			A.OuterShadow outerShadow2 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

			A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "000000" };
			A.Alpha alpha2 = new A.Alpha() { Val = 35000 };

			rgbColorModelHex12.Append( alpha2 );

			outerShadow2.Append( rgbColorModelHex12 );

			effectList2.Append( outerShadow2 );

			effectStyle2.Append( effectList2 );

			A.EffectStyle effectStyle3 = new A.EffectStyle();

			A.EffectList effectList3 = new A.EffectList();

			A.OuterShadow outerShadow3 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

			A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "000000" };
			A.Alpha alpha3 = new A.Alpha() { Val = 35000 };

			rgbColorModelHex13.Append( alpha3 );

			outerShadow3.Append( rgbColorModelHex13 );

			effectList3.Append( outerShadow3 );

			A.Scene3DType scene3DType1 = new A.Scene3DType();

			A.Camera camera1 = new A.Camera() { Preset = A.PresetCameraValues.OrthographicFront };
			A.Rotation rotation1 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 0 };

			camera1.Append( rotation1 );

			A.LightRig lightRig1 = new A.LightRig() { Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
			A.Rotation rotation2 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 1200000 };

			lightRig1.Append( rotation2 );

			scene3DType1.Append( camera1 );
			scene3DType1.Append( lightRig1 );

			A.Shape3DType shape3DType1 = new A.Shape3DType();
			A.BevelTop bevelTop1 = new A.BevelTop() { Width = 63500L, Height = 25400L };

			shape3DType1.Append( bevelTop1 );

			effectStyle3.Append( effectList3 );
			effectStyle3.Append( scene3DType1 );
			effectStyle3.Append( shape3DType1 );

			effectStyleList1.Append( effectStyle1 );
			effectStyleList1.Append( effectStyle2 );
			effectStyleList1.Append( effectStyle3 );

			A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

			A.SolidFill solidFill5 = new A.SolidFill();
			A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

			solidFill5.Append( schemeColor11 );

			A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

			A.GradientStopList gradientStopList3 = new A.GradientStopList();

			A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

			A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Tint tint4 = new A.Tint() { Val = 40000 };
			A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 350000 };

			schemeColor12.Append( tint4 );
			schemeColor12.Append( saturationModulation8 );

			gradientStop7.Append( schemeColor12 );

			A.GradientStop gradientStop8 = new A.GradientStop() { Position = 40000 };

			A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Tint tint5 = new A.Tint() { Val = 45000 };
			A.Shade shade5 = new A.Shade() { Val = 99000 };
			A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 350000 };

			schemeColor13.Append( tint5 );
			schemeColor13.Append( shade5 );
			schemeColor13.Append( saturationModulation9 );

			gradientStop8.Append( schemeColor13 );

			A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

			A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Shade shade6 = new A.Shade() { Val = 20000 };
			A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 255000 };

			schemeColor14.Append( shade6 );
			schemeColor14.Append( saturationModulation10 );

			gradientStop9.Append( schemeColor14 );

			gradientStopList3.Append( gradientStop7 );
			gradientStopList3.Append( gradientStop8 );
			gradientStopList3.Append( gradientStop9 );

			A.PathGradientFill pathGradientFill1 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
			A.FillToRectangle fillToRectangle1 = new A.FillToRectangle() { Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

			pathGradientFill1.Append( fillToRectangle1 );

			gradientFill3.Append( gradientStopList3 );
			gradientFill3.Append( pathGradientFill1 );

			A.GradientFill gradientFill4 = new A.GradientFill() { RotateWithShape = true };

			A.GradientStopList gradientStopList4 = new A.GradientStopList();

			A.GradientStop gradientStop10 = new A.GradientStop() { Position = 0 };

			A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Tint tint6 = new A.Tint() { Val = 80000 };
			A.SaturationModulation saturationModulation11 = new A.SaturationModulation() { Val = 300000 };

			schemeColor15.Append( tint6 );
			schemeColor15.Append( saturationModulation11 );

			gradientStop10.Append( schemeColor15 );

			A.GradientStop gradientStop11 = new A.GradientStop() { Position = 100000 };

			A.SchemeColor schemeColor16 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
			A.Shade shade7 = new A.Shade() { Val = 30000 };
			A.SaturationModulation saturationModulation12 = new A.SaturationModulation() { Val = 200000 };

			schemeColor16.Append( shade7 );
			schemeColor16.Append( saturationModulation12 );

			gradientStop11.Append( schemeColor16 );

			gradientStopList4.Append( gradientStop10 );
			gradientStopList4.Append( gradientStop11 );

			A.PathGradientFill pathGradientFill2 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
			A.FillToRectangle fillToRectangle2 = new A.FillToRectangle() { Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

			pathGradientFill2.Append( fillToRectangle2 );

			gradientFill4.Append( gradientStopList4 );
			gradientFill4.Append( pathGradientFill2 );

			backgroundFillStyleList1.Append( solidFill5 );
			backgroundFillStyleList1.Append( gradientFill3 );
			backgroundFillStyleList1.Append( gradientFill4 );

			formatScheme1.Append( fillStyleList1 );
			formatScheme1.Append( lineStyleList1 );
			formatScheme1.Append( effectStyleList1 );
			formatScheme1.Append( backgroundFillStyleList1 );

			themeElements1.Append( colorScheme1 );
			themeElements1.Append( fontScheme2 );
			themeElements1.Append( formatScheme1 );
			A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
			A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

			theme1.Append( themeElements1 );
			theme1.Append( objectDefaults1 );
			theme1.Append( extraColorSchemeList1 );

			themePart1.Theme = theme1;
		}

		// Generates content of worksheetPart1.
		public void GenerateWorksheetPart1Content( WorksheetPart worksheetPart1 ) {
			Worksheet worksheet1 = new Worksheet();
			worksheet1.AddNamespaceDeclaration( "r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships" );
			SheetDimension sheetDimension1 = new SheetDimension() { Reference = "A1:A4" };

			SheetViews sheetViews1 = new SheetViews();

			SheetView sheetView1 = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
			Selection selection1 = new Selection() { ActiveCell = "A8", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "A8" } };

			sheetView1.Append( selection1 );

			sheetViews1.Append( sheetView1 );
			SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties() { DefaultRowHeight = 15D };

			Columns columns1 = new Columns();
			Column column1 = new Column() { Min = (UInt32Value)1U, Max = (UInt32Value)1U, Width = 109.28515625D, Style = (UInt32Value)1U, CustomWidth = true };

			columns1.Append( column1 );

			SheetData sheetData1 = new SheetData();

			IEnumerator sharedEnumerator = _sharedStrings.GetEnumerator();
			int sharedStringIndex = 0;
			UInt32Value rowIndex = 1;
			while( sharedEnumerator.MoveNext() ) {
				Row row = new Row() { RowIndex = rowIndex, Spans = new ListValue<StringValue>() { InnerText = "1:1" } };
				Cell cell = new Cell() { CellReference = "A" + rowIndex.ToString(), StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
				Cell cell2 = new Cell();
				Cell cell3 = new Cell();
				CellValue cellValue = new CellValue();
				cellValue.Text = sharedStringIndex.ToString();
				cell.Append( cellValue );
				row.Append( cell );
				row.Append( cell2 );
				row.Append( cell3 );
				sheetData1.Append( row );
				sharedStringIndex++;
				rowIndex++;
			}


			//Row row1 = new Row() { RowIndex = (UInt32Value)1U, Spans = new ListValue<StringValue>() { InnerText = "1:1" } };

			//Cell cell1 = new Cell() { CellReference = "A1", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
			//CellValue cellValue1 = new CellValue();
			//cellValue1.Text = "0";

			//cell1.Append( cellValue1 );

			//row1.Append( cell1 );

			//Row row2 = new Row() { RowIndex = (UInt32Value)2U, Spans = new ListValue<StringValue>() { InnerText = "1:1" } };

			//Cell cell2 = new Cell() { CellReference = "A2", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
			//CellValue cellValue2 = new CellValue();
			//cellValue2.Text = "1";

			//cell2.Append( cellValue2 );

			//row2.Append( cell2 );

			//Row row3 = new Row() { RowIndex = (UInt32Value)3U, Spans = new ListValue<StringValue>() { InnerText = "1:1" } };

			//Cell cell3 = new Cell() { CellReference = "A3", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
			//CellValue cellValue3 = new CellValue();
			//cellValue3.Text = "2";

			//cell3.Append( cellValue3 );

			//row3.Append( cell3 );

			//Row row4 = new Row() { RowIndex = (UInt32Value)4U, Spans = new ListValue<StringValue>() { InnerText = "1:1" } };

			//Cell cell4 = new Cell() { CellReference = "A4", StyleIndex = (UInt32Value)1U, DataType = CellValues.SharedString };
			//CellValue cellValue4 = new CellValue();
			//cellValue4.Text = "3";

			//cell4.Append( cellValue4 );

			//row4.Append( cell4 );

			//sheetData1.Append( row1 );
			//sheetData1.Append( row2 );
			//sheetData1.Append( row3 );
			//sheetData1.Append( row4 );
			PageMargins pageMargins1 = new PageMargins() { Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
			PageSetup pageSetup1 = new PageSetup() { Orientation = OrientationValues.Portrait, HorizontalDpi = (UInt32Value)0U, VerticalDpi = (UInt32Value)0U, Id = "rId1" };

			worksheet1.Append( sheetDimension1 );
			worksheet1.Append( sheetViews1 );
			worksheet1.Append( sheetFormatProperties1 );
			worksheet1.Append( columns1 );
			worksheet1.Append( sheetData1 );
			worksheet1.Append( pageMargins1 );
			worksheet1.Append( pageSetup1 );

			worksheetPart1.Worksheet = worksheet1;
		}

		// Generates content of spreadsheetPrinterSettingsPart1.
		public void GenerateSpreadsheetPrinterSettingsPart1Content( SpreadsheetPrinterSettingsPart spreadsheetPrinterSettingsPart1 ) {
			System.IO.Stream data = GetBinaryDataStream( spreadsheetPrinterSettingsPart1Data );
			spreadsheetPrinterSettingsPart1.FeedData( data );
			data.Close();
		}

		// Generates content of sharedStringTablePart1.
		public void GenerateSharedStringTablePart1Content( SharedStringTablePart sharedStringTablePart1 ) {
			SharedStringTable sharedStringTable1 = new SharedStringTable() { Count = (UInt32Value)4U, UniqueCount = (UInt32Value)4U };

			IEnumerator sharedEnumerator = _sharedStrings.GetEnumerator();
			while( sharedEnumerator.MoveNext() ) {
				Text sharedText = new Text();
				sharedText.Text = ((String[])sharedEnumerator.Current)[1];
				SharedStringItem sharedStringItem = new SharedStringItem();
				sharedStringItem.Append( sharedText );
				sharedStringTable1.Append( sharedStringItem );
			}

			//SharedStringItem sharedStringItem1 = new SharedStringItem();
			//Text text1 = new Text();
			//text1.Text = "Log Item 1";

			//sharedStringItem1.Append( text1 );

			//SharedStringItem sharedStringItem2 = new SharedStringItem();
			//Text text2 = new Text();
			//text2.Text = "Log Item 2";

			//sharedStringItem2.Append( text2 );

			//SharedStringItem sharedStringItem3 = new SharedStringItem();
			//Text text3 = new Text();
			//text3.Text = "Log Item 3";

			//sharedStringItem3.Append( text3 );

			//SharedStringItem sharedStringItem4 = new SharedStringItem();
			//Text text4 = new Text();
			//text4.Text = "Log Item 5";

			//sharedStringItem4.Append( text4 );

			//sharedStringTable1.Append( sharedStringItem1 );
			//sharedStringTable1.Append( sharedStringItem2 );
			//sharedStringTable1.Append( sharedStringItem3 );
			//sharedStringTable1.Append( sharedStringItem4 );

			sharedStringTablePart1.SharedStringTable = sharedStringTable1;
		}

		private void SetPackageProperties( OpenXmlPackage document ) {
			document.PackageProperties.Creator = "ramsey";
			document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime( "2011-02-16T17:22:01Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind );
			document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime( "2011-02-16T17:31:26Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind );
			document.PackageProperties.LastModifiedBy = "ramsey";
		}

		#region Binary Data
		private string spreadsheetPrinterSettingsPart1Data = "XABcAHMAcwAtAGgAbwBtAGUALQAxAFwAQgByAG8AdABoAGUAcgAgAEgATAAtADIAMQA3ADAAVwAgAHMAAAAAAAEECAHcAMQJD5MBAAEAAQDqCm8IZAABAAcAWAIBAAEAWAIBAAAATABlAHQAdABlAHIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAGAf9QUklWACAAACSYBgBAABEBARAAABgAAAAAABAnECcQJwAAECcAAAAAAAAAAP8AAAAAAQAAAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC15wUAAQAFAAEAAAAAAAAAAAAAAQIAAAAAAAAAAABkAAAAAAAAAAAAAQABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQD6AogEAAAPAAAAAAAAAAEAAAEAAAAAAQAAACMHAQAAAAEAAQAAAAAAAAD//yAABwAHAAcAAAEAAAAAAACBAAAAAAAAAAAAAAACAAAABwAAAAAAAAAAGQAA5AwAAAAAAAC4CwAAcBcAAAAAAADcBQAAWAIAAAAAAAAAAAAAAAAAAP7/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3DAAA2gwAABAnAAAQJwAAECcAABAnAACgCgAAwgYAALgGAAAsBAAAQAEAANIAAAAAAAoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAP//AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAOQEAABTAFMALQBSAEEATQBTAEUAWQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD//////v////7////+/////v////7////+/////v////7////+/////v////7////+/////v////7////+/////v////7////+/////v////7////+//////////7///9FTkdMSVNIAAAAAAAAAAAAQgBSAFMAUAAyADAANwBBAC4ARQBYAEUAAAAAAAAAAABCAFIATABIAEwAQQA3AEEALgBEAEwATAAAAAAAAAAAAEIAUgBCADMATABBADcAQQAuAEQATABMAAAAAAAAAAAAQgBPADIAMQA3ADAAVwAuAEkATgBJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJAJAEAAAAAAAAAAAAAAAAAQD//wAAAAAOAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADGAIgAAABkAAAAAAABAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAYAAAD1////AAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAQQByAGkAYQBsAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD///8AAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA/////2QAAAAAAAAAAAAAgHIAYQBtAHMAZQB5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAABAAAAQlJMSEwwN0EuRExMAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEVPU0M=";

		private System.IO.Stream GetBinaryDataStream( string base64String ) {
			return new System.IO.MemoryStream( System.Convert.FromBase64String( base64String ) );
		}

		#endregion

	}
}

