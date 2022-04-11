export function Modal({ title, button, content, _key }) {
  return (
    <div>
      <span data-toggle="modal" data-target={`#Modal${_key}`}>
        {button}
      </span>
      <div
        className="modal fade"
        id={`Modal${_key}`}
        tabIndex="-1"
        role="dialog"
        aria-hidden="true"
      >
        <div className="modal-dialog modal-dialog-centered" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title">{title}</h5>
              <button
                type="button"
                className="close"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div className="modal-body">{content}</div>
          </div>
        </div>
      </div>
    </div>
  );
}
