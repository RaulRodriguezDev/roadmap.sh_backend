const parseNote = (req, res) => {
    res.send('Note parsed successfully')
}

const saveNote = (req, res) => {
    res.send('Note saved successfully')
}

const listNotes = (req,res) => {
    res.send('Notes listed successfully')
}

const getNote = (req, res) => {
    res.send('Note retrieved successfully')
}

export { parseNote, saveNote, listNotes, getNote }