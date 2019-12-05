# DevChallenge 2019 Final

C# task - Financial calculations

## Table of Content

- [Run](#run)
- [Usage](#usage)
  - [Unit tests](#unit-tests)
  - [Integration tests](#integration-tests)

## Run

```bash
# Go into the folder with solution and run:
docker-compose up
```

Service will be available on port 8888.


## Usage

When docker runs open http://localhost:8888 on your browser to see swagger docs and try api.

### Unit Tests

Use 'Run-Unit-Tests' cake task fro running unit tests.

For **Windows**
```bash
# Open PowerShell

# Go into the folder with solution and run:
./build/cake/build.ps1 -Script build/cake/build.cake -Target Run-Unit-Tests
```

For **Unix**:
```bash
# Go into the folder with solution and run:
$ ./build/cake/build.sh -s build/cake/build.cake --target=Run-Unit-Tests
```

### Integration Tests

Use 'Run-Integration-Tests' cake task fro running unit tests.

For **Windows**
```bash
# Open PowerShell

# Go into the folder with solution and run:
./build/cake/build.ps1 -Script build/cake/build.cake -Target=Run-Integration-Tests
```

For **Unix**:
```bash
# Go into the folder with solution and run:
$ ./build/cake/build.sh -s build/cake/build.cake --target=Run-Integration-Tests
```
