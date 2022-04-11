import { useEffect, useState } from "react";
import api from "../../../api";

export function ManipulacaoDiasFuncionamento() {
  const [diasFuncionamento, setDiasFuncionamento] = useState([]);

  useEffect(() => {
    getTurnos();
  }, []);

  function getTurnos() {
    api.get("/api/v1/diasFuncionamento").then((res) => {
      setDiasFuncionamento(res.data);
    });
  }

  function updateDiasFuncionamento(e) {
    const data = {
      diasFuncionamento,
    };

    api
      .put("/api/v1/diasFuncionamento", data)
      .catch((err) => {
        // TODO: tratar erros
      });
  }

  return (
    <div>
      <form>
        <div className="form-group">
          <label>Dias da semana que o restaurante funciona: </label>
          <div className="align-center ml-2">
            {diasFuncionamento.map((day, i) => (
              <div key={i} className="form-check m-2">
                <input
                  type="checkbox"
                  className="form-check-input"
                  id={`check${i}`}
                  checked={day.ativo}
                  onChange={(e) => {
                    const newDiasFuncionamento = [...diasFuncionamento];
                    newDiasFuncionamento[i].ativo = e.target.checked;

                    setDiasFuncionamento(newDiasFuncionamento);
                  }}
                ></input>
                <label className="form-check-label" htmlFor={`check${i}`}>
                  {day.descricaoDia}
                </label>
              </div>
            ))}
          </div>
        </div>
        <div className="d-flex justify-content-end">
          <button
            type="submit"
            onClick={(e) => updateDiasFuncionamento(e)}
            className="btn btn-primary"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  );
}
