import express from 'express'
import { getNote, listNotes, parseNote, saveNote } from '../controllers/notesController.js'
const notesRouter = express.Router()

notesRouter.post('/parse', parseNote)
notesRouter.get('/list', listNotes)
notesRouter.post('/save', saveNote)
notesRouter.get('/:id', getNote)

export default notesRouter