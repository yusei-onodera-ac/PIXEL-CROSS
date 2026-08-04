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
    Gacha/        ガチャ抽選・アイテム
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

## 実装状況（雛形段階）
- [x] コアループの骨組み：週単位ターン進行（1年=48週）、シーズンフェーズ自動判定
- [x] データモデル：選手5能力値、チーム、ライバル校20校（強豪5校含む）、作戦・戦術
- [x] 各システムのスケルトン：練習/スタミナ、スカウト、ガチャ、試合の簡易計算、チュートリアル進行、セーブ/ロード
- [ ] Unityシーン・UI（未着手。上記スクリプトを呼び出す画面が必要）
- [ ] ドット絵アセット（プレースホルダー無し）
- [ ] 試合のトップダウン・アクション表現（現状は数値ベースの簡易シミュレーションのみ）
- [ ] 課金（IAP）実装
- [ ] 非同期フレンド対戦（OB防衛チーム）のネットワーク部分

## 注意
本フォルダは空のGitHubリポジトリ (`https://github.com/yusei-onodera-ac/PIXEL-CROSS.git`) に対応する雛形です。まだリモートへはpushしていません。
