# 社内コミュニケーションログ

社内AI組織 全体就業規則に基づく、指示・報告の記録。

---

2026-08-04 [Dev] 社長より「PIXEL CROSS」企画書を受領。Unity(C#)での雛形作成の承認を得て、プロジェクト構成案（フォルダ構成・技術スタック）を提示。
2026-08-04 [Dev] 社長承認（Unityで進める）を受け、Unityプロジェクト雛形を作成。内容：Packages/manifest.json、ProjectSettings、コアスクリプト（GameManager/TurnManager/SaveSystem）、データモデル（PlayerData/TeamData/RivalSchoolData 20校）、練習・スカウト・ガチャ・試合の各システムスケルトン、チュートリアル進行、多言語対応(日/英/仏)の骨組み、README、企画書コピー(docs/GameDesignDocument.md)を作成完了。次工程はUnityシーン・UI実装とドット絵アセット制作。
2026-08-04 [Dev] 社長より「実際にコーディング作業に移ってほしい、仕様の甘い点は都度確認してほしい」と指示。Unity非依存のロジック層（週次ループ統合・新入部員自動生成・4年生引退/プロスカウト判定・リーグ戦日程抽選・インカレ8校トーナメント）を実装。セーブ/ロード時に新入部員が重複生成される設計バグを発見し、TurnManager.LoadStateを追加して修正。
2026-08-04 [Dev] 社長より2層通貨システムの指示（基本硬貨=試合勝利/イベント報酬、上位硬貨=課金/連続ログイン報酬、上位硬貨→基本硬貨の交換、ガチャはチケットor上位硬貨、施設強化に基本硬貨を使用）を受け、TeamDataの通貨フィールド再設計、CurrencyExchangeSystem、LoginBonusSystem（10日サイクルの連続ログイン報酬）、GachaSystemのチケット/上位硬貨2way課金対応、FacilitySystem骨組みを実装。施設の具体的な効果は未設計のため要確認。コミット(88a7b17)・push済み。
2026-08-04 [Dev] ガチャ/ショップ入手アイテムの保管場所が未実装だった点を報告し、社長承認を得てインベントリシステムを実装。GachaItemをPixelCross.Gacha専用からPixelCross.Data.Itemへ移動（ショップ等からも共用できるデータ層に格上げ）、InventorySystem（取得・使用）、GameManagerへのガチャ抽選+自動格納の統合を実施。アイテム種別ごとの効果（SkillBook→テクニック等）は暫定割り当てのため要確認。
2026-08-04 [Dev] 社長より「進め方は任せる。起動時にTHGロゴ（白背景・よくあるやつ）→タイトル画面（タイトル/はじめから/つづきから/設定）を実装してほしい」と指示。SplashScreenController（フェードイン→ホールド→フェードアウト、タップスキップ）とTitleScreenController（つづきからボタンはセーブ有無で活性切替）を実装。ただしUnity Editorがこの環境に無いため実際の.unityシーンファイルは作成できず、READMEに手動セットアップ手順を追記。THGロゴの画像素材は未受領のため、仮のテキストロゴで進めることで社長承認済み。コミット(a108098)・push済み。
2026-08-04 [Dev] 社長がUnityダウンロード中とのことで、その間にロジック層を継続。(1) GameManagerが生のTutorialStepのみ保持しTutorialManager本体が未使用だった不整合を発見・修正（TutorialManager.LoadState追加）。(2) タイトル画面「はじめから」の遷移先だったTeamSetupController（大学名/監督名入力→StartNewGame→Gameplayへ）を実装。(3) 未設計だったアイテムショップ（ItemShopSystem、固定カタログ3種・基本硬貨で購入）を実装。すべてスクリプトのみでシーン未作成。
2026-08-04 [Dev] 社長より「今回日本で開催された世界大会に出てる国の言語を増やしてほしい」と指示。Web検索で確認したところ、直近(2026/7/24〜8/2, 東京)開催の「2026 World Lacrosse Women's Championship」参加16カ国（日本/イングランド/イスラエル/スコットランド/ウェールズ/アイルランド/ドイツ/チェコ/中華台北/オーストラリア/フィリピン/アメリカ/カナダ/ハウデノサウニー/プエルトリコ/アルゼンチン）が該当すると判断。SupportedLanguageに西/独/チェコ/ヘブライ/繁体中国語/フィリピノを追加（英語圏・スペイン語圏は既存言語に統合）。ヘブライ語はRTL表示のためTextMeshPro標準では崩れる旨、Haudenosaunee（ハウデノサウニー）は単一言語コードが無いため未対応の旨をREADMEに明記。
