import { useEffect, useState } from "react";
import api from "../../../api";

export function ManipulacaoMesa({ id }) {
  const [nomeMesa, setNomeMesa] = useState("");
  const [quantCadeiras, setQuantCadeiras] = useState("");
 
  useEffect(() => {
    api.get("/api/v1/mesa").then((res) => {
      const mesa = res.data.find((el) => el.id === id);

      setNomeMesa(mesa?.nomeMesa || "");
      setQuantCadeiras(mesa?.quantCadeiras || "");
    });
  }, [id]);


  function createMesa(e) {
    const data = {
      nomeMesa,
      quantCadeiras,
    };

    api.post("/api/v1/mesa", data).catch((err) => {
      console.log(nomeMesa)
      console.log(quantCadeiras)
      console.log(id)
      // TODO: tratar erros
    });
  }
  function updateMesa(e) {
    const data = {
      id,
      nomeMesa,
      quantCadeiras,
    };

    api.put("/api/v1/mesa", data).catch((err) => {
      // TODO: tratar erros
    });
  }

  return (
    <div>
      <form>
        <div className="form-group">
          <label htmlFor="nomeMesa">Nome da Mesa</label>
          <input
            type="text"
            className="form-control"
            id="nomeMesa"
            placeholder="Insira o Nome da Mesa"
            value={nomeMesa}
            onChange={(e) => setNomeMesa(e.target.value)}
          ></input>
        </div>
        <div className="form-group">
          <label htmlFor="quantCadeiras">Quantidade de Cadeiras</label>
          <input
            type="number"
            className="form-control"
            id="quantCadeiras"
            value={quantCadeiras}
            onChange={(e) => setQuantCadeiras(e.target.value)}
          ></input>
        </div>

        <div className="d-flex justify-content-end">
          <button
            type="submit"
            onClick={(e) => (id ? updateMesa(e) : createMesa(e))}
            className="btn btn-primary"
          >
            Salvar
          </button>
        </div>
      </form>
    </div>
  );
}

