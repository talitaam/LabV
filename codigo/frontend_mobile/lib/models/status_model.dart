class StatusModel {
  final String id;
  final String descricao;

  const StatusModel({
    this.id,
    this.descricao,
  });

  factory StatusModel.fromJson(Map<String, dynamic> json) => StatusModel(
        id: json["id"].toString(),
        descricao: json["descricao"],
      );

  Map<String, dynamic> toJson() => {
        "id": int.parse(id),
        "descricao": descricao,
      };
}
