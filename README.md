# Consumer-Producer Shared Memory Program

## **Overview**

This program implements a **Consumer-Producer** pattern using shared memory between threads. The main thread reads data from input files, processes the data using worker threads, and writes the results to an output file. The program uses two monitors (synchronized shared data structures) to manage unprocessed data and computed results.

- **Data Monitor**: Stores the unprocessed data that worker threads will consume.
- **Result Monitor**: Stores the results computed by the worker threads, ensuring the results are kept in sorted order.

The program handles thread synchronization and blocking mechanisms to ensure data is processed in a thread-safe manner. It is designed to process a list of data entries with a custom structure and filter the results based on a defined criterion.

## **Project Structure**

1. **Main Thread**:
   - Reads input data from files and stores it in a local array.
   - Spawns a configurable number of worker threads.
   - Writes data to the data monitor and waits for worker threads to complete processing.
   - Writes the sorted results from the result monitor to an output file.

2. **Worker Threads**:
   - Each worker thread takes an item from the data monitor.
   - Processes the data and computes a result based on a custom function.
   - Inserts valid results into the result monitor, keeping the results sorted.
   - Waits for data if the monitor is empty and continues processing until all data is handled.

3. **Monitors**:
   - **Data Monitor**: A synchronized shared array to store unprocessed data. The main thread writes data to it, and workers consume the data.
   - **Result Monitor**: A synchronized shared array to store results. It remains sorted as new results are inserted by workers.

## **Files**

- **Input Files**: Three JSON files containing at least 25 elements each, named according to the format:
  - `Group_LastnameF_L1_dat_1.json`: Contains data that matches the filter criteria.
  - `Group_LastnameF_L1_dat_2.json`: Contains some data that matches the filter criteria.
  - `Group_LastnameF_L1_dat_3.json`: Contains no data that matches the filter criteria.
  
- **Output File**: The computed results, sorted and written to a table in a text file.

## **Requirements**

- Java (or your preferred programming language with thread synchronization support)
- A JSON parsing library for reading input files (e.g., Jackson, Gson)
- Threading support via `ExecutorService` or similar tools

## **Features**

- Multi-threaded processing with thread synchronization.
- Sorting results in the result monitor.
- Customizable data processing and filtering criteria.
- Asynchronous data reading, processing, and writing.

## **Usage**

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/consumer-producer-shared-memory.git
   ```

2. Compile and run the program:
   ```bash
   java MainClass
   ```

3. The program will read input from the provided JSON files, spawn worker threads, and output the processed results to a text file.

## **License**

This project is licensed under the MIT License

