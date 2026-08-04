# PIXEL CROSS（ピクセル・クロス）

大学ラクロス育成・経営シミュレーション。企画書は [docs/GameDesignDocument.md](docs/GameDesignDocument.md) を参照。

## 技術スタック
- Unity **2022.3 LTS** (`2022.3.50f1` を想定。[ProjectSettings/ProjectVersion.txt](ProjectSettings/ProjectVersion.txt) で変更可能)
- C#
- ビルドターゲット：iOS優先、Android/PC等クロスプラットフォーム対応
- 主要パッケージ（[Packages/manifest.json](Packages/manifest.json)）：2D Sprite / 2D Pixel Perfect / 2D Animation / TextMeshPro / Unity Localization / Input System

## セットアップ
1. Unity Hub で本フォルダを「プロジェクトを開く」から追加する。
2. 指定バージョンのUnity Editorがない場合はUnity Hubが自動でインストールを促す。
3. 初回起動時にパッケージの解決（Package Manager）が走るため数分待つ。

## フォルダ構成
```
Assets/
  Scripts/
    Core/         GameManager, TurnManager, SeasonPhase（週/シーズン進行の中核）
    Data/         PlayerData, PlayerStats, TeamData, RivalSchoolData 等のデータモデル
    Training/     練習メニューとステータス成長ロジック
    Scouting/     高校生スカウト（夏季解禁・年2枚チケット）
    Gacha/        ガチャ抽選・アイテム（ガチャチケット/上位硬貨消費）
    Economy/      2層通貨（基本硬貨/上位硬貨）の交換・連続ログインボーナス
    Facility/     施設強化（骨組みのみ・効果未接続）
    Inventory/    所持アイテムの保管・使用（ガチャ/ショップ入手品）
    Match/        試合結果シミュレーション（暫定ロジック）
    Tutorial/     初回チュートリアルのステップ管理
    Localization/ 多言語対応（日本語/英語/フランス語）
    SaveLoad/     セーブデータ定義とJSON保存/読込
    UI/           起動ロゴ(Splash)・タイトル画面のコントローラー
  Scenes/         Unityシーン（未作成・要追加。下記「Boot/Titleシーンの作成手順」参照）
  Sprites/        ドット絵素材（Players / UI / Field）
  Prefabs/        プレハブ
  Resources/Data/ ScriptableObject等のランタイムデータ
  Audio/          効果音・BGM
  Localization/   Unity Localization用テーブル
docs/             企画書・設計資料
company/logs/     社内AI組織の作業ログ
```

## 実装状況
- [x] コアループの骨組み：週単位ターン進行（1年=48週）、シーズンフェーズ自動判定
- [x] データモデル：選手5能力値、チーム、ライバル校20校（強豪5校含む）、作戦・戦術
- [x] 各システムのスケルトン：練習/スタミナ、スカウト、ガチャ、試合の簡易計算、チュートリアル進行、セーブ/ロード
- [x] 週次ループ統合（[GameManager.cs](Assets/Scripts/Core/GameManager.cs)）：新入部員自動生成・学年進級、4年生引退＋プロスカウト判定、リーグ戦日程抽選（[LeagueScheduleGenerator.cs](Assets/Scripts/Match/LeagueScheduleGenerator.cs)）、インカレ8校トーナメント（[IntercollegiateSystem.cs](Assets/Scripts/Match/IntercollegiateSystem.cs)）、セーブ/ロード時の日程復元
- [x] 2層通貨・ログインボーナス：基本硬貨/上位硬貨（[TeamData.cs](Assets/Scripts/Data/TeamData.cs)）、上位硬貨→基本硬貨の交換（[CurrencyExchangeSystem.cs](Assets/Scripts/Economy/CurrencyExchangeSystem.cs)）、連続ログイン日数に応じた上位硬貨付与（[LoginBonusSystem.cs](Assets/Scripts/Economy/LoginBonusSystem.cs)）、ガチャチケット/上位硬貨の2way課金（[GachaSystem.cs](Assets/Scripts/Gacha/GachaSystem.cs)）
- [x] インベントリ：ガチャ/ショップ入手アイテムの保管・使用（[InventorySystem.cs](Assets/Scripts/Inventory/InventorySystem.cs)）。アイテム種別ごとの効果先（SkillBook→テクニック、TrainingGear→ボディ等）は暫定割り当て
- [x] 起動ロゴ・タイトル画面のコントローラー：[SplashScreenController.cs](Assets/Scripts/UI/SplashScreenController.cs)（白背景フェードイン→ホールド→フェードアウト、タップでスキップ）、[TitleScreenController.cs](Assets/Scripts/UI/TitleScreenController.cs)（はじめから/つづきから/設定、セーブ有無でつづきからボタンの活性切替）。**スクリプトのみで、実際のUnityシーン(.unity)は未作成**（後述の手順を参照）
- [ ] Unityシーン本体（Boot/Title含め未作成。上記コントローラーをアタッチする画面が必要）
- [ ] ドット絵アセット（プレースホルダー無し）
- [ ] 試合のトップダウン・アクション表現（現状は数値ベースの簡易シミュレーションのみ）
- [ ] 課金（IAP）実装本体（上位硬貨の購入導線）
- [ ] アイテムショップ本体（基本硬貨でのアイテム購入カタログ・価格は未設計。現状はガチャ経由の入手のみ）
- [ ] 施設強化（[FacilitySystem.cs](Assets/Scripts/Facility/FacilitySystem.cs)）：レベル/コストの骨組みのみで、効果（練習効率・スタミナ回復等への影響）は未設計
- [ ] 非同期フレンド対戦（OB防衛チーム）のネットワーク部分
- [ ] 評価バランス（勝敗による知名度/ランキング変動値、通貨獲得量・交換レート・ガチャ価格は全て仮の数値。要調整）

## Boot/Titleシーンの作成手順（Unity Editorでの作業）
このリポジトリのコード側にはUnity Editorが無いため、`.unity`シーンファイル自体は用意されていません。以下の手順でUnity Editor上に作成し、上記スクリプトをアタッチしてください。

1. `Assets/Scenes/`に `Boot.unity` と `Title.unity` を新規作成（File > New Scene > 保存）。
2. **Boot.unity**:
   - 空のGameObject「GameManager」を作成し、[GameManager.cs](Assets/Scripts/Core/GameManager.cs)をアタッチ（`DontDestroyOnLoad`済みなので他シーンでも生存）。
   - Canvas（Screen Space - Overlay）を作成し、その下に全画面Image（色=白）を配置。
   - さらにその下（またはCanvas自体）に空のGameObject「SplashRoot」を作り、`CanvasGroup`と[SplashScreenController.cs](Assets/Scripts/UI/SplashScreenController.cs)をアタッチ。
   - SplashRootの子にロゴ用のImage（THGロゴ画像 or 仮のTextMeshPro「THG」テキスト）を配置。
3. **Title.unity**:
   - Canvasを作成し、ゲームタイトルロゴ/テキストを配置。
   - Button×3（はじめから／つづきから／設定）を配置し、空のGameObjectに[TitleScreenController.cs](Assets/Scripts/UI/TitleScreenController.cs)をアタッチして、Inspectorで3つのButtonを割り当てる。
4. File > Build Settings > Scenes In Build に `Boot`（index 0）→ `Title` の順で追加。
5. `SplashScreenController`のInspectorで遷移先シーン名（デフォルト`Title`）とフェード秒数を確認・調整。

※「はじめから」「設定」の遷移先（`TeamSetup`/`Settings`シーン）はまだ存在しません。押しても何も起きないので、それぞれのシーンを作るまでは想定内の挙動です。

## 注意
- 本フォルダは空のGitHubリポジトリ (`https://github.com/yusei-onodera-ac/PIXEL-CROSS.git`) に対応する雛形です。
- ロジック層（Unity Editor不要な部分）を優先実装中。Unity Editorでのシーン/UI作成は別途対応が必要です。
