# DevChallenge 2019 Final

C# task - Financial calculations

## Table of Content

- [Run](#run)
- [Usage](#usage)
  - [Unit tests](#unit-tests)
  - [Integration tests](#integration-tests)
- [Improvements](#improvemetns)

## Run

```bash
# Go into the folder with solution and run:
docker-compose up
```

Service will be available on port 8080.


## Usage

When docker runs open http://localhost:8080 on your browser to see swagger docs and try api.

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
./build/cake/build.ps1 -Script build/cake/build.cake -Target Run-Integration-Tests
```

For **Unix**:
```bash
# Go into the folder with solution and run:
$ ./build/cake/build.sh -s build/cake/build.cake --target=Run-Integration-Tests
```

### Improvements

There are a lot points to improve:
- DataAccess
  - Simplify db seed(DbInitializer class)
  - Use base repository class for all repositories
  - Implement two types of repository: readonly and read/write repository
- Unit tests
  - Simplify unit tests(e.g. GetAveragePriceQueryHandlerTests can be much smaller)
  - Cover more classes/cases with tests
- Integration tests
  - Add integration tests for endpoints(not only db)
  - Can be simplified