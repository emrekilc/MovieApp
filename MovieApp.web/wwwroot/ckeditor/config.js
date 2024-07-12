CKEDITOR.editorConfig = function (config) {
    // Ekstra eklenti
    config.extraPlugins = 'pastebase64';

    // Edit�r y�ksekli�i
    config.height = 650;

    // Karakter entity ayarlar�
    config.basicEntities = false;
    config.entities_latin = false;
    config.entities_greek = false;

    // Dil ayar�
    config.language = 'tr';

    // Ara� �ubu�unu sadele�tirme
    config.toolbar = [
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
        { name: 'editing', items: ['Scayt'] },
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'RemoveFormat'] },
        { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote'] },
        { name: 'links', items: ['Link', 'Unlink'] },
        { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
        { name: 'styles', items: ['Styles', 'Format'] },
        { name: 'tools', items: ['Maximize'] }
    ];
};
