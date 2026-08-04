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
  Scenes/         Unityシーン（未作成・要追加）
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
- [ ] Unityシーン・UI（未着手。上記スクリプトを呼び出す画面が必要）
- [ ] ドット絵アセット（プレースホルダー無し）
- [ ] 試合のトップダウン・アクション表現（現状は数値ベースの簡易シミュレーションのみ）
- [ ] 課金（IAP）実装本体（上位硬貨の購入導線）
- [ ] アイテムショップ本体（基本硬貨でのアイテム購入カタログ・価格は未設計。現状はガチャ経由の入手のみ）
- [ ] 施設強化（[FacilitySystem.cs](Assets/Scripts/Facility/FacilitySystem.cs)）：レベル/コストの骨組みのみで、効果（練習効率・スタミナ回復等への影響）は未設計
- [ ] 非同期フレンド対戦（OB防衛チーム）のネットワーク部分
- [ ] 評価バランス（勝敗による知名度/ランキング変動値、通貨獲得量・交換レート・ガチャ価格は全て仮の数値。要調整）

## 注意
- 本フォルダは空のGitHubリポジトリ (`https://github.com/yusei-onodera-ac/PIXEL-CROSS.git`) に対応する雛形です。
- ロジック層（Unity Editor不要な部分）を優先実装中。Unity Editorでのシーン/UI作成は別途対応が必要です。
