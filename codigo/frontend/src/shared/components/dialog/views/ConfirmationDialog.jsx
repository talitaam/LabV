import { Button, Modal } from "react-bootstrap";
import { useState } from "react";

export function ConfirmationDialog({
  toProceed,
  icon,
  description,
  nameProceedButton,
}) {
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  return (
    <>
      <span className="cursor-pointer theme-icon" onClick={handleShow}>
        {icon}
      </span>
      <Modal show={show} onHide={handleClose} keyboard={false} centered>
        <Modal.Header closeButton>
          <Modal.Title>Confirme sua ação</Modal.Title>
        </Modal.Header>
        <Modal.Body>{description}</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Cancelar
          </Button>
          <Button onClick={() => toProceed()} variant="primary">
            {nameProceedButton}
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

ConfirmationDialog.propTypes = {};
