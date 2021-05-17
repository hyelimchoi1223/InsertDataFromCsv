# InsertDataFromCsv
 
### 목적
- 샘플로 받은 약 1000개 가량 되는 CSV 파일을 데이터베이스에 넣는 작업을 하기 위해 만듬.

### 주의사항
- `App.Config`에서 데이터베이스 사전 설정 필요함.

### 테스트 타입 추가 시
 1. `EnumData/EnumData.cs`에 데이터 타입 추가.
 2. `AppendWorker` 폴더에 추가 데이터 타입의 Worker 필요.
 3. 2번에서 생성한 Worker는 `Abstract.AppendWorkerBase`의 상속이 필요.
 4. `Program.cs` 의 `AppendHelper`에 추가한 데이터 타입의 `switch-case` 추가.
