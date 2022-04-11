class TurnoModel {
  final String id;
  final String descricao;
  final String horarioInicio;
  final String horarioFim;

  const TurnoModel({
    this.id,
    this.descricao,
    this.horarioInicio,
    this.horarioFim,
  });

  factory TurnoModel.fromJson(Map<String, dynamic> json) => TurnoModel(
        id: json["id"].toString(),
        descricao: json["descricao"],
        horarioInicio: json["horarioInicio"],
        horarioFim: json["horarioFim"],
      );

  Map<String, dynamic> toJson() => {
        "id": int.parse(id),
        "descricao": descricao,
        "horarioInicio": horarioInicio,
        "horarioFim": horarioFim,
      };
}
