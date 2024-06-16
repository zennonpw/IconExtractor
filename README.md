# IconExtractor

IconExtractor is a tool developed in C# to extract icons from the `iconlist` within .dds files of the Perfect World game using the names provided in the .txt file.

<p align="center">
  <img src="https://i.imgur.com/TGe3ZR2.gif" Alt="Example gif">
</p>

## Features

- **Open .dds File**: Allows you to select a .dds file containing the icons.
- **Open .txt File**: Allows you to select a .txt file with information about the position and size of the icons in the .dds file.
- **Select Output Folder**: Allows you to choose the folder where the extracted icons will be saved.
- **Extract Icons**: Extracts the icons from the .dds file and saves them as .png files in the selected output folder.


## How to Use

### Step 1: Open the .dds File

Click the "Open .dds File" button to select the .dds file that contains the icons. The program will update the status to indicate that the .dds file is ready for extraction.

### Step 2: Open the .txt File

Click the "Open .txt File" button to select the .txt file that contains information about the icons. The .txt file should be formatted with the coordinates and sizes of the icons.

### Step 3: Select the Output Folder

Click the "Select Output Folder" button to choose the folder where the extracted icons will be saved.

### Step 4: Extract Icons

Click the "Extract Icons" button to start the extraction process. The icons will be saved in the output folder as .png files. The status will update to "Done" when the extraction is complete.

## Code Structure

### Main Classes

- `MainWindow`: The main class that manages the user interface and user interactions.
- `DDSReader`: Reads and decodes image files in the DDS (DirectDraw Surface) format. It provides methods to obtain image information, such as width, height, mipmap levels, and color masks.
- `DDSImage`: It provides functionalities to load .dds images, convert them into bitmaps, and obtain dimensions of .dds files.

### Main Methods

- `OpenDds()`: Opens the dialog to select the .dds file.
- `OpenTxt()`: Opens the dialog to select the .txt file.
- `SelectOutputFolder()`: Opens the dialog to select the output folder.
- `ExtractIcons()`: Performs the icon extraction and saves them as .png files.
- `CalculateIconPositionFromDdsFile(int iconIndex)`: Calculates the position of the icons in the .dds file based on the index.

## User Interface

The user interface consists of buttons to open the .dds and .txt files, select the output folder, and extract the icons. It also includes labels to display the status and paths of the selected files.
<p align="center">
  <img src="https://i.imgur.com/ZUJONhI.png" alt="Interface"/>
  </p>

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/zennonpw/IconExtractor/edit/master/LICENSE) file for more details.
