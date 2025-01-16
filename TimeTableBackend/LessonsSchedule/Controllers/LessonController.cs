﻿using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TimeTableBackend.LessonsSchedule.Dtos;
using TimeTableBackend.LessonsSchedule.Entities;
using TimeTableBackend.LessonsSchedule.Services;

namespace TimeTableBackend.LessonsSchedule.Controllers;

[ApiController]
[Route("api/lesson")]
[DisplayName("Lesson Controller")]
[Produces("application/json")]
public class LessonController : ControllerBase
{
    private readonly ILessonsService _lessonsService;
    private readonly IMapper _mapper;

    public LessonController(ILessonsService lessonsService, IMapper mapper)
    {
        _lessonsService = lessonsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all lessons.
    /// </summary>
    /// <returns>A list of lessons.</returns>
    /// <response code="200">Returns the list of lessons.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LessonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LessonDto>>> Get()
    {
        var lessons = await _lessonsService.GetAllAsync();

        return Ok(_mapper.Map<IEnumerable<LessonDto>>(lessons));
    }

    /// <summary>
    /// Creates a new lesson.
    /// </summary>
    /// <param name="dto">The data for the lesson to create.</param>
    /// <returns>The created lesson.</returns>
    /// <response code="200">Returns the created lesson.</response>
    [HttpPost]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<LessonDto>> Post(CreateLessonDto dto)
    {
        var lessonEntity = _mapper.Map<Lesson>(dto);

        await _lessonsService.CreateAsync(lessonEntity);

        var createdLesson = _mapper.Map<LessonDto>(lessonEntity);

        return Ok(createdLesson);
    }

    /// <summary>
    /// Updates an existing lesson by ID.
    /// </summary>
    /// <param name="id">The ID of the lesson to update.</param>
    /// <param name="dto">The updated lesson data.</param>
    /// <response code="204">The lesson was successfully updated.</response>
    /// <response code="404">Lesson not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put(string id, UpdateLessonDto dto)
    {
        var lessonEntity = await _lessonsService.GetByIdAsync(id);

        if (lessonEntity is null)
        {
            return NotFound();
        }

        _mapper.Map(dto, lessonEntity);

        await _lessonsService.UpdateAsync(lessonEntity);

        return NoContent();
    }

    /// <summary>
    /// Deletes a lesson by ID.
    /// </summary>
    /// <param name="id">The ID of the lesson to delete.</param>
    /// <response code="204">The lesson was successfully deleted.</response>
    /// <response code="404">Lesson not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(string id)
    {
        var lessonEntity = await _lessonsService.GetByIdAsync(id);

        if (lessonEntity is null)
        {
            return NotFound();
        }

        await _lessonsService.DeleteAsync(lessonEntity);

        return NoContent();
    }
}