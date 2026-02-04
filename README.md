# ホットペッパービューティー検索自動化システム

## プロジェクト概要

ホットペッパービューティーのサロン検索を自動化し、検索条件に基づいたサロン情報の取得・ソート・保存を行うWebアプリケーションです。

### 目的
- ジャンル・地域・価格帯・評価などの条件でサロンを検索
- 取得したサロン情報を評価・口コミ数・価格帯でソート
- 検索履歴の保存と再利用

### 注意事項
**本システムはWebスクレイピングを使用します。以下の点に十分注意してください：**
- ホットペッパービューティーの利用規約を遵守すること
- 個人の非商用利用に限定すること
- サーバーに過度な負荷をかけないこと（適切なディレイを設定）
- 法的リスクを理解した上で使用すること

---

## 採用技術スタック

### バックエンド
- **.NET 8 Web API**
  - ASP.NET Core Minimal API または Controller-based API
  - 非同期処理による高パフォーマンス
  
- **Entity Framework Core 8**
  - Code First Migrations
  - LINQ による型安全なクエリ
  
- **データベース**
  - 開発環境: SQLite
  - 本番環境: PostgreSQL または SQL Server

### フロントエンド（選択肢）
- **選択肢A: React 18 + TypeScript**（推奨）
  - Vite（ビルドツール）
  - TanStack Query（サーバー状態管理）
  - Material-UI または Ant Design（UIコンポーネント）
  
- **選択肢B: Blazor WebAssembly**
  - C#による統一開発
  - .NETエコシステム活用

### スクレイピング関連
- **AngleSharp** または **HtmlAgilityPack**
  - HTML/CSSセレクタ解析
  
- **Polly**
  - リトライポリシー、サーキットブレーカー

### ユーティリティ
- **AutoMapper**: DTO ↔ Entity マッピング
- **FluentValidation**: 入力検証
- **Serilog**: 構造化ログ

---

## ディレクトリ構成（予定）

```
HotPepperBeautySearchSystem/
│
├── src/
│   ├── Web/                                    # プレゼンテーション層
│   │   ├── HotPepperSearch.WebAPI/            # Web API プロジェクト
│   │   │   ├── Controllers/
│   │   │   │   ├── SalonController.cs
│   │   │   │   ├── MasterDataController.cs
│   │   │   │   └── HealthController.cs
│   │   │   ├── DTOs/
│   │   │   │   ├── SearchRequestDto.cs
│   │   │   │   ├── SalonResponseDto.cs
│   │   │   │   └── SortRequestDto.cs
│   │   │   ├── Middleware/
│   │   │   │   └── GlobalExceptionHandler.cs
│   │   │   ├── Program.cs
│   │   │   └── appsettings.json
│   │   │
│   │   └── HotPepperSearch.Web/               # React SPA または Blazor
│   │       ├── src/
│   │       │   ├── components/
│   │       │   ├── pages/
│   │       │   ├── hooks/
│   │       │   ├── services/
│   │       │   └── types/
│   │       └── package.json
│   │
│   ├── Application/                            # アプリケーション層
│   │   └── HotPepperSearch.Application/
│   │       ├── Services/
│   │       │   ├── SalonSearchService.cs
│   │       │   ├── SalonSortService.cs
│   │       │   ├── SearchHistoryService.cs
│   │       │   └── MasterDataService.cs
│   │       ├── Interfaces/
│   │       │   └── ISalonSearchService.cs
│   │       └── Common/
│   │           └── Result.cs                   # 汎用結果型
│   │
│   ├── Domain/                                 # ドメイン層
│   │   └── HotPepperSearch.Domain/
│   │       ├── Entities/
│   │       │   ├── Salon.cs
│   │       │   └── SearchHistory.cs
│   │       ├── ValueObjects/
│   │       │   ├── SearchCondition.cs
│   │       │   └── SortOption.cs
│   │       ├── Enums/
│   │       │   ├── Genre.cs
│   │       │   ├── SortField.cs
│   │       │   └── SortOrder.cs
│   │       └── Repositories/                   # インターフェース定義
│   │           ├── ISalonRepository.cs
│   │           └── ISearchHistoryRepository.cs
│   │
│   └── Infrastructure/                         # インフラストラクチャ層
│       └── HotPepperSearch.Infrastructure/
│           ├── Data/
│           │   ├── ApplicationDbContext.cs
│           │   ├── Configurations/             # EF Core エンティティ設定
│           │   │   ├── SalonConfiguration.cs
│           │   │   └── SearchHistoryConfiguration.cs
│           │   └── Migrations/                 # EF Core マイグレーション
│           ├── Repositories/                   # Repository実装
│           │   ├── SalonRepository.cs
│           │   └── SearchHistoryRepository.cs
│           ├── Scraping/                       # スクレイピング関連
│           │   ├── Interfaces/
│           │   │   ├── IScrapingService.cs
│           │   │   ├── IHtmlParserService.cs
│           │   │   └── IDelayService.cs
│           │   ├── Services/
│           │   │   ├── HotPepperBeautyScrapingService.cs
│           │   │   ├── AngleSharpHtmlParserService.cs
│           │   │   └── RandomDelayService.cs
│           │   └── Configuration/
│           │       └── ScrapingSettings.cs     # CSSセレクタ等の設定
│           └── Logging/
│               └── SerilogConfiguration.cs
│
├── tests/                                      # テストプロジェクト
│   ├── HotPepperSearch.UnitTests/
│   ├── HotPepperSearch.IntegrationTests/
│   └── HotPepperSearch.E2ETests/
│
├── docs/                                       # ドキュメント
│   ├── architecture.md                         # アーキテクチャ設計書
│   ├── api-specification.md                    # API仕様書
│   └── deployment.md                           # デプロイ手順
│
├── HotPepperBeautySearchSystem.sln            # ソリューションファイル
└── README.md
```

---

## 実装ルール

### 1. 命名規則

#### メソッド名
- **動作が明確にわかる命名**を徹底
- 動詞から始める（例: `GetSalonsByCondition`, `SaveSearchHistory`）
- 非同期メソッドは `Async` サフィックスを付ける
- ブール値を返すメソッドは `Is`, `Has`, `Can` で始める

**良い例:**
```csharp
Task<List<Salon>> SearchSalonsByConditionAsync(SearchCondition condition);
bool HasRating(Salon salon);
void ValidateSearchCondition(SearchCondition condition);
```

**悪い例:**
```csharp
Task<List<Salon>> Process(SearchCondition condition);  // 何をするか不明
bool Check(Salon salon);  // 何をチェックするか不明
void Do(SearchCondition condition);  // 抽象的すぎる
```

#### クラス名
- 責務を明確に表現
- サフィックスで種類を示す
  - Service: `SalonSearchService`
  - Repository: `SalonRepository`
  - Controller: `SalonController`
  - Dto: `SearchRequestDto`

### 2. DTO（Data Transfer Object）の必須使用

**原則: 層間のデータ転送は必ずDTOを使用**

```csharp
// ✅ 良い例: DTOを使用
public async Task<SalonResponseDto> GetSalonById(Guid id)
{
    var salon = await _repository.GetByIdAsync(id);
    return _mapper.Map<SalonResponseDto>(salon);
}

// ❌ 悪い例: エンティティを直接返却
public async Task<Salon> GetSalonById(Guid id)
{
    return await _repository.GetByIdAsync(id);  // エンティティの内部構造が露出
}
```

**DTOの配置場所:**
- Web API層: `WebAPI/DTOs/`
- Application層とDomain層の間: 必要に応じて `Application/DTOs/`

### 3. 外部API呼び出しはInfrastructure層に集約

**原則: すべての外部システムへのアクセスはInfrastructure層で抽象化**

```csharp
// ✅ 良い例: Application層ではインターフェースに依存
public class SalonSearchService
{
    private readonly IScrapingService _scrapingService;  // Infrastructure層の実装
    
    public async Task<List<Salon>> SearchAsync(SearchCondition condition)
    {
        var rawData = await _scrapingService.ScrapeAsync(condition);
        // ビジネスロジック...
    }
}

// ❌ 悪い例: Application層で直接HttpClientを使用
public class SalonSearchService
{
    private readonly HttpClient _httpClient;  // 外部依存が露出
    
    public async Task<List<Salon>> SearchAsync(SearchCondition condition)
    {
        var response = await _httpClient.GetAsync("https://...");  // NG
    }
}
```

**Infrastructure層での実装:**
```csharp
// Infrastructure/Scraping/Services/HotPepperBeautyScrapingService.cs
public class HotPepperBeautyScrapingService : IScrapingService
{
    private readonly HttpClient _httpClient;
    private readonly IHtmlParserService _parser;
    
    // 外部API呼び出しの詳細はここに隠蔽
}
```

### 4. サードパーティ依存は極力減らす

**原則: 必要最小限のライブラリのみ使用**

**許可されるサードパーティライブラリ:**
- .NET公式ライブラリ（Microsoft製）
- 広く使われている成熟したライブラリ
  - AngleSharp / HtmlAgilityPack（HTML解析）
  - Polly（リトライ制御）
  - AutoMapper（マッピング）
  - Serilog（ロギング）

**避けるべき:**
- マイナーなライブラリ
- メンテナンスされていないライブラリ
- 複数の選択肢がある場合、より標準的なものを選択

**依存管理:**
```csharp
// ✅ 良い例: 標準ライブラリで実装可能ならそれを使う
var delay = TimeSpan.FromSeconds(Random.Shared.Next(3, 7));
await Task.Delay(delay);

// ❌ 悪い例: 不要なライブラリ追加
// await SomeExoticDelayLibrary.WaitAsync(...);
```

### 5. エラーハンドリング

**原則: 例外は適切にキャッチし、ログを記録**

```csharp
public async Task<Result<List<Salon>>> SearchAsync(SearchCondition condition)
{
    try
    {
        var salons = await _scrapingService.ScrapeAsync(condition);
        _logger.LogInformation("Successfully scraped {Count} salons", salons.Count);
        return Result<List<Salon>>.Success(salons);
    }
    catch (HttpRequestException ex)
    {
        _logger.LogError(ex, "Network error during scraping");
        return Result<List<Salon>>.Failure("ネットワークエラーが発生しました");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Unexpected error during scraping");
        return Result<List<Salon>>.Failure("予期しないエラーが発生しました");
    }
}
```

### 6. 非同期処理

**原則: I/O処理は必ず非同期化**

```csharp
// ✅ 良い例
public async Task<Salon> GetSalonByIdAsync(Guid id)
{
    return await _dbContext.Salons.FindAsync(id);
}

// ❌ 悪い例
public Salon GetSalonById(Guid id)
{
    return _dbContext.Salons.Find(id);  // 同期処理
}
```

### 7. 依存性注入（DI）

**原則: コンストラクタインジェクションを使用**

```csharp
public class SalonSearchService
{
    private readonly ISalonRepository _repository;
    private readonly IScrapingService _scrapingService;
    private readonly ILogger<SalonSearchService> _logger;
    
    public SalonSearchService(
        ISalonRepository repository,
        IScrapingService scrapingService,
        ILogger<SalonSearchService> logger)
    {
        _repository = repository;
        _scrapingService = scrapingService;
        _logger = logger;
    }
}
```

---

## 今回のスコープ

### ✅ スコープ内（実装する機能）

#### 1. 検索機能
- ジャンル選択（ヘアサロン、ネイル、エステ、リラク、アイビューティー）
- 地域選択（都道府県、市区町村）
- 価格帯フィルタ
- 評価フィルタ（例: 3.0以上）

#### 2. データ取得
- ホットペッパービューティーからのスクレイピング
- サロン情報の取得
  - サロン名
  - エリア
  - 最寄り駅
  - 評価
  - 口コミ数
  - 価格帯
- データのクレンジング・正規化

#### 3. ソート機能
- 評価順（昇順/降順）
- 口コミ数順（昇順/降順）
- 価格帯順（昇順/降順）

#### 4. データ保存
- 検索結果のデータベース保存
- 検索履歴の保存

#### 5. API提供
- RESTful API エンドポイント
- 検索実行API
- ソート実行API
- 履歴取得API
- マスタデータ取得API

#### 6. 基本的なフロントエンド
- 検索条件入力フォーム
- 検索結果表示（テーブル or カード形式）
- ソート機能UI
- 検索履歴表示

#### 7. 安全策
- アクセスディレイ（3-7秒のランダム）
- リトライロジック
- エラーハンドリング
- ロギング

#### 8. 基本的なテスト
- 単体テスト（主要ビジネスロジック）
- 統合テスト（Repository層）

---

### ❌ スコープ外（実装しない機能）

#### 1. UIデザイン最適化
- レスポンシブデザインの細かい調整
- アニメーション・トランジション
- 高度なUX設計
- アクセシビリティ対応（最低限は考慮）

#### 2. 管理者用ダッシュボード
- システム監視画面
- スクレイピング実行状況の可視化
- ログビューアー
- 統計・分析機能

#### 3. 認証・認可
- ユーザー登録・ログイン機能
- JWT認証
- ロールベースアクセス制御
- ※単一ユーザー前提で開発

#### 4. 高度なデータ分析
- サロンのトレンド分析
- 価格帯の統計情報
- レコメンド機能

#### 5. 通知機能
- メール通知
- プッシュ通知
- Slack連携

#### 6. スケジューリング機能
- 定期実行バッチ
- cron設定UI
- ※手動実行のみ対応

#### 7. データエクスポート
- CSV/Excelエクスポート
- PDF生成
- ※データベースからの直接取得で代替

#### 8. マルチテナント対応
- 複数ユーザーの分離
- ユーザーごとのデータ管理

#### 9. 本番運用環境の構築
- Docker化
- CI/CDパイプライン
- クラウドデプロイ
- ※ローカル環境での動作を優先

#### 10. パフォーマンス最適化
- キャッシング戦略の高度化
- データベースインデックス最適化
- 非同期バックグラウンド処理
- ※基本的な実装のみ

---

## 開発環境セットアップ

### 必要な環境
- .NET 8 SDK
- Node.js 18+ （Reactを選択する場合）
- Visual Studio 2022 / Rider / VS Code
- Git

### セットアップ手順
```bash
# リポジトリクローン
git clone <repository-url>
cd HotPepperBeautySearchSystem

# バックエンド
cd src/Web/HotPepperSearch.WebAPI
dotnet restore
dotnet ef database update
dotnet run

# フロントエンド（React選択時）
cd src/Web/HotPepperSearch.Web
npm install
npm run dev
```

---

## 設定ファイル

### appsettings.json（例）
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=hotpepper.db"
  },
  "ScrapingSettings": {
    "BaseUrl": "https://beauty.hotpepper.jp/",
    "MinDelay": 3,
    "MaxDelay": 7,
    "PageDelay": 5,
    "MaxRetries": 3,
    "MaxPagesPerSearch": 10,
    "UserAgent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

---

## APIエンドポイント（予定）

| Method | Endpoint | 説明 |
|--------|----------|------|
| POST | `/api/salons/search` | サロン検索実行 |
| GET | `/api/salons/{id}` | サロン詳細取得 |
| POST | `/api/salons/sort` | ソート実行 |
| GET | `/api/salons/history` | 検索履歴取得 |
| GET | `/api/master/genres` | ジャンル一覧取得 |
| GET | `/api/master/prefectures` | 都道府県一覧取得 |
| GET | `/api/master/cities/{prefId}` | 市区町村一覧取得 |
| GET | `/api/health` | ヘルスチェック |

---

## 開発の進め方

### Phase 1: 基盤構築
- [ ] プロジェクト構成作成
- [ ] Domain層実装（Entity, ValueObject, Enum）
- [ ] データベース設計・Migration
- [ ] Repository インターフェース定義

### Phase 2: Infrastructure層実装
- [ ] スクレイピング機能実装
- [ ] HTMLパーサー実装
- [ ] ディレイ・リトライ機能
- [ ] Repository実装

### Phase 3: Application層実装
- [ ] SalonSearchService
- [ ] SalonSortService
- [ ] SearchHistoryService

### Phase 4: Web API実装
- [ ] Controller実装
- [ ] DTO定義
- [ ] エラーハンドリング
- [ ] バリデーション

### Phase 5: フロントエンド実装
- [ ] 検索フォーム
- [ ] 結果表示
- [ ] ソート機能
- [ ] 履歴表示

### Phase 6: テスト・ドキュメント
- [ ] 単体テスト
- [ ] 統合テスト
- [ ] APIドキュメント
- [ ] README更新

---

## ライセンス

このプロジェクトは個人学習目的で作成されています。
ホットペッパービューティーの利用規約を遵守し、商用利用は行わないでください。

---

## 注意事項・免責事項

1. **本システムの使用は自己責任で行ってください**
2. ホットペッパービューティーの利用規約を必ず確認してください
3. 過度なアクセスはサーバーに負荷をかけます。適切なディレイを設定してください
4. スクレイピング対象サイトの仕様変更により動作しなくなる可能性があります
5. 法的リスクを理解した上で使用してください

---

## 貢献

プルリクエストは歓迎しますが、以下の点を守ってください：
- 実装ルールに従ったコード
- 適切なテストの追加
- ドキュメントの更新

---

## 連絡先

質問や提案がある場合は、Issueを作成してください。
