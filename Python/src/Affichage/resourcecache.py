# -*- coding: utf8 -*-
"""Resource Caching Module

Most game projects require graphics, sound and other resources to be loaded
multiple times - a sprite's image might be loaded for each instance of that
sprite. Normally, you'll write some sort of resource management system to ensure
that all these identical resources aren't kept in memory at once.

This module wraps pygame's resource loading functions.  It ensures that only one
copy of any given resource will be in memory at any time - further requests for
that same resource will get a reference to the loaded copy.  This behavoir can
be overridden by specifying that the resource loading be forced - all clients of
that resource will then refer to the new version.  The resource can be manually
removed from memory with the clear_* set of functions.

This code might be useful if a game is going to be loading many copies of a
given disk resource.  Most of the time, you'd write your own cache; now you can
just use this one.

Remember to call the 'set path' functions before trying to load anything;
otherwise, you'll get a ValueError.
"""

__author__ = "David Clark"
__copyright__ = "Copyright (c) 2001 David Clark"
__license__ = "Public Domain"
__version__ = "1.0"


import pygame, os

__images = {}
__fonts = {}
__sounds = {}

def get_image(filename, force_reload = 0):
    """
    get_image(filename, force_reload = 1) --> surface
    Call this function instead of pygame.image.load - it will load the image
    from the disk the first time, then just return a reference to the copy each
    subsequent time.  This function does no colorkey setting or pixel format
    conversion; you'll have to do that manually, if you wish.
    """

    if (force_reload == 1 or filename not in list(__images.keys())):
        try:
            surface = pygame.image.load(filename)
        except pygame.error:
            raise IOError from "File " + filename + " not found."
        __images[filename] = surface
        return surface
    else:
        return __images[filename]

def has_image(filename):
    """
    has_image(filename) --> Boolean
    Returns true if the image is in memory, false if it has to be loaded from
    disk.
    """
    return filename in __images

def clear_image(filename):
    """
    clear_image(filename) --> Boolean
    Eliminates the image from memory.  Subsequent calls will load it from the
    disk.  Returns True if the resource was found in memory, False if it Wasn't.
    Use this to reduce the memory footprint, if you're sure you won't be needing
    the resource again.
    """

    try:
        del __images[filename]
        return 1
    except KeyError:
        return 0


def get_font(filename, size, force_reload = 0):
    """
    get_font(filename, size, force_reload = 1) --> surface
    Call this function instead of pygame.font.Font - it will load the font from
    the disk the first time, then just return a reference to the copy each
    subsequent time.
    """
    if (force_reload == 1 or filename not in list(__fonts.keys())):
        try:
            font = pygame.font.Font( filename, size)
        except pygame.error:
            raise IOError from "File " + filename + " not found."
        __fonts[filename] = font
        return font
    else:
        return __fonts[filename]

def has_font(filename):
    """
    has_font(filename) --> Boolean
    Returns true if the font is in memory, false if it has to be loaded from
    disk.
    """
    return filename in __fonts

def clear_font(filename):
    """
    clear_font(filename) --> Boolean
    Eliminates the font from memory.  Subsequent calls will load it from the
    disk.  Returns True if the resource was found in memory, False if it Wasn't.
    Use this to reduce the memory footprint, if you're sure you won't be needing
    the resource again.
    """
    try:
        del __fonts[filename]
        return 1
    except KeyError:
        return 0

def get_sound(filename, force_reload = 0):
    """
    get_sound(filename, force_reload = 1) --> sound
    Call this function instead of pygame.mixer.Sound - it will load the sound
    from the disk the first time, then just return a reference to the copy each
    subsequent time.
    """
    if (force_reload == 1 or filename not in list(__fonts.keys())):
        try:
            sound = pygame.mixer.Sound(filename)
        except pygame.error:
            raise IOError from "File " + filename + " not found."
        __sounds[filename] = sound
        return sound
    else:
        return __sounds[filename]

def has_sound(filename):
    """
    has_sound(filename) --> Boolean
    Returns true if the sound is in memory, false if it has to be loaded from
    disk.
    """
    return filename in __sounds

def clear_sound(filename):
    """
    clear_sound(filename) --> Boolean
    Eliminates the sound from memory.  Subsequent calls will load it from the
    disk.  Returns True if the resource was found in memory, False if it Wasn't.
    Use this to reduce the memory footprint, if you're sure you won't be needing
    the resource again.
    """
    try:
        del __sounds[filename]
        return 1
    except KeyError:
        return 0
