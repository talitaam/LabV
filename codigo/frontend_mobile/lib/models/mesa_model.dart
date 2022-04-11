class MesaModel {
  final String id;
  final String quantCadeiras;
  final String nomeMesa;

  const MesaModel({
    this.id,
    this.quantCadeiras,
    this.nomeMesa,
  });

  factory MesaModel.fromJson(Map<String, dynamic> json) => MesaModel(
        id: json["id"].toString(),
        quantCadeiras: json["quantCadeiras"].toString(),
        nomeMesa: json["nomeMesa"],
      );

  Map<String, dynamic> toJson() => {
        // Id será cadastrado pelo banco, mas podemos precisar para update
        "id": int.parse(id),
        "quantCadeiras": quantCadeiras,
        "nomeMesa": nomeMesa,
      };
}
