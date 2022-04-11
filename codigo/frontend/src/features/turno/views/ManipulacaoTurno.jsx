import { useEffect, useState } from "react";
import api from "../../../api";

export function ManipulacaoTurno({ id }) {
  const [descricao, setDescricao] = useState("");
  const [horarioInicio, setHorarioInicio] = useState("");
  const [horarioFim, setHorarioFim] = useState("");

  useEffect(() => {
    api.get("/api/v1/turno").then((res) => {
      const turno = res.data.find((el) => el.id === id);

      setDescricao(turno?.descricao || "");
      setHorarioInicio(turno?.horarioInicio || "");
      setHorarioFim(turno?.horarioFim || "");
    });
  }, [id]);

  function createTurno(e) {
    const data = {
      descricao,
      horarioInicio,
      horarioFim,
    };

    api.post("/api/v1/turno", data).catch((err) => {
      // TODO: tratar erros
    });
  }
  function updateTurno(e) {
    const data = {
      id,
      descricao,
      horarioInicio,
      horarioFim,
    };

    api.put("/api/v1/turno", data).catch((err) => {
      // TODO: tratar erros
    });
  }

  return (
    <div>
      <form>
        <div className="form-group">
          <label htmlFor="descricaoTurno">Nome do turno</label>
          <input
            type="text"
            className="form-control"
            id="descricaoTurno"
            placeholder="Insira a descrição do turno"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
          ></input>
        </div>
        <div className="form-group">
          <label htmlFor="horarioInicio">Horário de Inicio</label>
          <input
            type="time"
            className="form-control"
            id="horarioInicio"
            value={horarioInicio}
            onChange={(e) => setHorarioInicio(e.target.value)}
          ></input>
        </div>
        <div className="form-group">
          <label htmlFor="horarioFim">Horário de Fim</label>
          <input
            type="time"
            className="form-control"
            id="horarioFim"
            value={horarioFim}
            onChange={(e) => setHorarioFim(e.target.value)}
          ></input>
        </div>
        <div className="d-flex justify-content-end">
          <button
            type="submit"
            onClick={(e) => (id ? updateTurno(e) : createTurno(e))}
            className="btn btn-primary"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  );
}
